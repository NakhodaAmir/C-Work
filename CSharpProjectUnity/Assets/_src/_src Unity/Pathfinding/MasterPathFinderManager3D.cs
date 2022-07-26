namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using GenericGraphs.GraphSearch;
            using GenericGraphs.GraphSearch.DynamicGraphSearch;
            using MirJan.Helpers;
            using Helpers;
            using System;
            using System.Collections.Concurrent;
            using System.Collections.Generic;
            using System.Threading;
            using UnityEngine;

            public abstract class MasterPathFinderManager3D<Type> : SingletonBehaviour<MasterPathFinderManager3D<Type>>, IGraphSearchable<MasterPathFinderManager3D<Type>.Node, Type>
            {
                #region Enum
                public enum SearchType
                {
                    ASTAR_SEARCH,
                    DIJKSTRA_SEARCH,
                    GREEDYBESTFIRST_SEARCH,
                    FRINGE_SEARCH
                }
                #endregion

                #region Public Variables
                [Header("Path Finding Attributes")]
                public SearchType searchType;

                [Header("MultiThreading")]
                [Range(1f, 16f)]
                public int ThreadCount = 3;
                #endregion

                protected override void Awake()
                {
                    base.Awake();

                    CreateGraph();

                    PathFinderManager.Instance.Awake();
                }

                private void Update()
                {
                    PathFinderManager.Instance.Update();
                }

                private void LateUpdate()
                {
                    PathFinderManager.Instance.LateUpdate();
                }

                #region Graph 3D Interface
                public abstract Node NodeFromWorldPoint(Vector3 worldPosition);
                public abstract void CreateGraph();
                #endregion

                #region Graph Searchable Interface
                public abstract List<Node> GetNeighbourNodes(Node node);
                public abstract float HeuristicCost(Type valueA, Type valueB);
                public abstract float NodeTraversalCost(Type valueA, Type valueB);
                #endregion

                #region Path Finder Manager
                public class PathFinderManager : SingletonClass<PathFinderManager>
                {                   
                    readonly int threadCount = MasterPathFinderManager3D<Type>.Instance.ThreadCount;

                    readonly Lazy<PathFinder> pathFinder = new Lazy<PathFinder>();

                    readonly ConcurrentQueue<PathRequest?> pathRequests = new ConcurrentQueue<PathRequest?>();
                    readonly ConcurrentQueue<PathResult?> pathResults = new ConcurrentQueue<PathResult?>();

                    public void Awake()
                    {
                        PathFindingAgent.RequestPath = EnqueuePathRequest;
                    }

                    public void Update()
                    {
                        while(pathResults.Count > 0)
                        {
                            pathResults.TryDequeue(out PathResult? pathResult);

                            if(pathResult != null)
                            {
                                pathResult?.Callback(pathResult?.Path, (bool)pathResult?.IsSuccess);
                            }
                        }
                    }

                    public void LateUpdate()
                    {
                        while(pathRequests.Count > 0 && pathResults.Count < threadCount)
                        {
                            pathRequests.TryDequeue(out PathRequest? pathRequest);

                            if(pathRequest != null)
                            {
                                Thread thread = new Thread(() => pathFinder.Value.FindPath((PathRequest)pathRequest, EnqueuePathResult));
                                thread.Start();
                            }
                        }
                    }

                    public void EnqueuePathRequest(PathRequest pathRequest)
                    {
                        pathRequests.Enqueue(pathRequest);
                    }

                    public void EnqueuePathResult(PathResult pathResult)
                    {
                        pathResults.Enqueue(pathResult);
                    }

                }
                #endregion

                #region Path Finder
                public class PathFinder
                {
                    readonly GraphSearcher<Node, Type> graphSearcher;
                    readonly MasterPathFinderManager3D<Type> graph;

                    public PathFinder()
                    {
                        graph = Instance;

                        if (graph.searchType == SearchType.ASTAR_SEARCH)
                        {
                            graphSearcher = new AStarSearch<Node, Type>(graph, false);
                        }
                        else if (graph.searchType == SearchType.DIJKSTRA_SEARCH)
                        {
                            graphSearcher = new DijkstraSearch<Node, Type>(graph, false);
                        }
                        else if (graph.searchType == SearchType.GREEDYBESTFIRST_SEARCH)
                        {
                            graphSearcher = new GreedyBestFirstSearch<Node, Type>(graph, false);
                        }
                        else//if(Graph.searchType == SearchType.FRINGE_SEARCH)
                        {
                            graphSearcher = new FringeSearch<Node, Type>(graph, false);
                        }
                    }

                    public void FindPath(PathRequest pathRequest, Action<PathResult> callBack)
                    {
                        lock (graphSearcher)
                        {
                            graphSearcher.Initialize(NodeFromWorldPoint(pathRequest.StartPosition), NodeFromWorldPoint(pathRequest.TargetPosition));

                            while (graphSearcher.IsRunning) graphSearcher.Step();

                            callBack(new PathResult(GetWayPoints(graphSearcher.PathList), graphSearcher.IsSucceeded, pathRequest.Callback));
                        }             
                    }

                    Node NodeFromWorldPoint(Vector3 position)
                    {
                        lock (graph)
                        {
                            return graph.NodeFromWorldPoint(position);
                        }
                    }

                    Vector3[] GetWayPoints(List<Node> pathList)
                    {
                        Vector3[] wayPoints = new Vector3[pathList.Count];

                        int i = 0;

                        foreach (Node node in pathList)
                        {
                            wayPoints[i] = node.WorldPosition;
                            i++;
                        }

                        return wayPoints;
                    }
                }
                #endregion

                #region Node
                public class Node : GraphSearchableNode<Type>
                {
                    public Vector3 WorldPosition { get; private set; }
                    public bool IsWalkable { get; private set; }

                    public Node(Vector3 worldPosition, bool isWalkable, Type value, float pCost = 0) : base(value, pCost)
                    {
                        WorldPosition = worldPosition;
                        IsWalkable = isWalkable;
                    }
                }
                #endregion

            }
        }
    }
}

