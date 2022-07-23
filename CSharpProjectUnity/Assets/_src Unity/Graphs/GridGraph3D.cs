namespace MirJan
{
    namespace Unity
    {
        namespace Graphs
        {
            using GenericGraphs.GraphSearch;
            using GenericGraphs.GraphSearch.DynamicGraphSearch;
            using System.Collections;
            using System.Collections.Generic;
            using UnityEngine;
            using Utilities;

            public class GridGraph3D : MonoBehaviour, IGraphSearchable<GridGraph3D.Node, Vector2Int>
            {
                #region Debug
                [Header("Debug")]
                public bool displayGridGizmos;
                #endregion

                #region Enum
                public enum HeuristicType
                {
                    MANHATTEN_DISTANCE,
                    OCTILE_DISTANCE,
                    CHEBYSHEV_DISTANCE
                }
                #endregion

                #region Public Variables
                [Header("Public Variables")]
                public HeuristicType heuristicType;
                public LayerMask obstacleMask;
                public Vector2Int gridWorldSize;
                public float nodeRadius;
                public GraphTerrainType[] walkableRegions;
                public int obstacleProximityPenalty = 10;
                [Range(0f, 5f)]
                public int blurSize = 0;

                public Transform seeker, target;
                #endregion

                #region Private Variables
                Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int, int>();
                LayerMask walkableMask;
                Node[,] grid;
                float nodeDiameter;
                int gridSizeX, gridSizeY;
                int minPenalty = int.MaxValue;
                int maxPenalty = int.MinValue;
                #endregion

                FringeSearch<Node, Vector2Int> aStar;

                void Awake()
                {
                    nodeDiameter = nodeRadius * 2;
                    gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
                    gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

                    foreach (GraphTerrainType region in walkableRegions)
                    {
                        walkableMask.value |= region.terrainMask.Value;
                        walkableRegionsDictionary.Add((int)Mathf.Log(region.terrainMask.Value, 2), region.terrainPenalty);
                    }

                    CreateGraph();

                    aStar = new FringeSearch<Node, Vector2Int>(this)
                    {
                        OnStart = OnStarted,
                        OnSuccess = OnSuccess,
                    };
                }
                void Start()
                {
                    StartCoroutine(SolvePath());
                }

                List<Node> path;
                void OnStarted()
                {
                    path = null;
                }
                void OnSuccess()
                {
                    path = new List<Node>();

                    var currentNode = aStar.CurrentNode;

                    while (!Equals(currentNode.Location, aStar.SourceNode))
                    {
                        path.Add((Node)currentNode.Location);
                        //currentNode = currentNode.Parent;
                        currentNode = aStar.Cache[currentNode];
                    }

                    path.Reverse();
                }

                IEnumerator SolvePath()
                {
                    aStar.Initialize(NodeFromWorldPoint(seeker.position), NodeFromWorldPoint(target.position));

                    while (aStar.IsRunning)
                    {
                        aStar.Step();

                        yield return new WaitForSeconds(0.01f);
                    }
                }

                void CreateGraph()
                {
                    grid = new Node[gridSizeX, gridSizeY];

                    Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

                    for (int x = 0; x < gridSizeX; x++)
                    {
                        for (int y = 0; y < gridSizeY; y++)
                        {
                            Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);

                            bool isWalkable = !Physics.CheckSphere(worldPoint, nodeRadius, obstacleMask);

                            int movementPenalty = 0;

                            Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
                            if (Physics.Raycast(ray, out RaycastHit hit, 100, walkableMask))
                            {
                                walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
                            }

                            if (!isWalkable)
                            {
                                movementPenalty += obstacleProximityPenalty;
                            }

                            grid[x, y] = new Node(worldPoint, isWalkable, new Vector2Int(x, y), movementPenalty);
                        }
                    }

                    BlurPenaltyMap(blurSize);
                }

                void BlurPenaltyMap(int blurSize)
                {
                    int kernelSize = blurSize * 2 + 1;
                    int kernelExtents = (kernelSize - 1) / 2;

                    int[,] penaltiesHorizontalPass = new int[gridSizeX, gridSizeY];
                    int[,] penaltiesVerticalPass = new int[gridSizeX, gridSizeY];

                    for (int y = 0; y < gridSizeY; y++)
                    {
                        for (int x = -kernelExtents; x <= kernelExtents; x++)
                        {
                            int sampleX = Mathf.Clamp(x, 0, kernelExtents);
                            penaltiesHorizontalPass[0, y] += (int)grid[sampleX, y].GraphSearcherNode.PCost;
                        }

                        for (int x = 1; x < gridSizeX; x++)
                        {
                            int removeIndex = Mathf.Clamp(x - kernelExtents - 1, 0, gridSizeX);
                            int addIndex = Mathf.Clamp(x + kernelExtents, 0, gridSizeX - 1);

                            penaltiesHorizontalPass[x, y] = (int)(penaltiesHorizontalPass[x - 1, y] - grid[removeIndex, y].GraphSearcherNode.PCost + grid[addIndex, y].GraphSearcherNode.PCost);
                        }
                    }

                    for (int x = 0; x < gridSizeX; x++)
                    {
                        for (int y = -kernelExtents; y <= kernelExtents; y++)
                        {
                            int sampleY = Mathf.Clamp(y, 0, kernelExtents);
                            penaltiesVerticalPass[x, 0] += penaltiesHorizontalPass[x, sampleY];
                        }

                        int blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, 0] / (kernelSize * kernelSize));
                        grid[x, 0].GraphSearcherNode.PCost = blurredPenalty;

                        for (int y = 1; y < gridSizeY; y++)
                        {
                            int removeIndex = Mathf.Clamp(y - kernelExtents - 1, 0, gridSizeY);
                            int addIndex = Mathf.Clamp(y + kernelExtents, 0, gridSizeY - 1);

                            penaltiesVerticalPass[x, y] = penaltiesVerticalPass[x, y - 1] - penaltiesHorizontalPass[x, removeIndex] + penaltiesHorizontalPass[x, addIndex];
                            blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, y] / (kernelSize * kernelSize));
                            grid[x, y].GraphSearcherNode.PCost = blurredPenalty;

                            if (blurredPenalty > maxPenalty)
                            {
                                maxPenalty = blurredPenalty;
                            }
                            if (blurredPenalty < minPenalty)
                            {
                                minPenalty = blurredPenalty;
                            }
                        }
                    }

                }

                #region Public Methods
                public List<Node> GetNeighbourNodes(Node node)
                {
                    List<Node> neighbours = new List<Node>();

                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (x == 0 && y == 0) continue;

                            if (heuristicType == HeuristicType.MANHATTEN_DISTANCE)
                            {
                                if (x == -1 && y == 1) continue;
                                if (x == -1 && y == -1) continue;
                                if (x == 1 && y == 1) continue;
                                if (x == 1 && y == -1) continue;
                            }

                            int checkX = node.Value.x + x;
                            int checkY = node.Value.y + y;

                            if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY && grid[checkX, checkY].IsWalkable)
                            {
                                neighbours.Add(grid[checkX, checkY]);
                            }
                        }
                    }

                    return neighbours;
                }

                public float HeuristicCost(Vector2Int valueA, Vector2Int valueB)
                {
                    if (heuristicType == HeuristicType.MANHATTEN_DISTANCE)
                    {
                        return GraphUtilities2D.GridUtilities.ManhattenDistance(valueA, valueB);
                    }
                    else if (heuristicType == HeuristicType.OCTILE_DISTANCE)
                    {
                        return GraphUtilities2D.GridUtilities.OctileDistance(valueA, valueB);
                    }
                    else//if(heuristicType == HeuristicType.CHEBYSHEV_DISTANCE)
                    {
                        return GraphUtilities2D.GridUtilities.ChebyshevDistance(valueA, valueB);
                    }
                }

                public float NodeTraversalCost(Vector2Int valueA, Vector2Int valueB)
                {
                    return HeuristicCost(valueA, valueB);
                }

                public Node NodeFromWorldPoint(Vector3 worldPosition)
                {
                    float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
                    float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
                    percentX = Mathf.Clamp01(percentX);
                    percentY = Mathf.Clamp01(percentY);

                    int x = Mathf.Clamp(Mathf.FloorToInt(gridSizeX * percentX), 0, gridSizeX - 1);
                    int y = Mathf.Clamp(Mathf.FloorToInt(gridSizeY * percentY), 0, gridSizeY - 1);
                    return grid[x, y];
                }
                #endregion

                #region Node
                public class Node : GraphSearchableNode<Vector2Int>
                {
                    public Vector3 WorldPosition { get; private set; }
                    public bool IsWalkable { get; private set; }

                    public Node(Vector3 worldPosition, bool isWalkable, Vector2Int value, float pCost = 0) : base(value, pCost)
                    {
                        WorldPosition = worldPosition;
                        IsWalkable = isWalkable;
                    }
                }
                #endregion

                #region Debug
                void OnDrawGizmos()
                {
                    Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
                    if (grid != null && displayGridGizmos)
                    {
                        foreach (Node n in grid)
                        {

                            Gizmos.color = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(minPenalty, maxPenalty, n.GraphSearcherNode.PCost));
                            Gizmos.color = n.IsWalkable ? Gizmos.color : Color.red;

                            //if (aStar.ClosedList.Contains(n.GraphSearcherNode))
                            //{
                            //    Gizmos.color = Color.blue;
                            //}

                            //if (aStar.OpenList.Contains(n.GraphSearcherNode))
                            //{
                            //    Gizmos.color = Color.yellow;
                            //}

                            if (aStar.Cache.ContainsKey(n.GraphSearcherNode))
                            {
                                Gizmos.color = Color.blue;
                            }

                            if (aStar.FringeList.Contains(n.GraphSearcherNode))
                            {
                                Gizmos.color = Color.yellow;
                            }

                            if (path != null)
                            {
                                
                                if (path.Contains(n))
                                {
                                    Gizmos.color = Color.black;
                                }
                            }

                            Gizmos.DrawCube(n.WorldPosition, new Vector3(0.9f, 0.1f, 0.9f) * nodeDiameter);
                        }
                    }
                }
                #endregion
            }
        }
    }
}
