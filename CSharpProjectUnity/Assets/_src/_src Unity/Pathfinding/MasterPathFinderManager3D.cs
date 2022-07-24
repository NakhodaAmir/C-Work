namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using GenericGraphs.GraphSearch;
            using GenericGraphs.GraphSearch.DynamicGraphSearch;
            using Helpers;
            using System;
            using System.Collections.Concurrent;
            using System.Collections.Generic;
            using System.Threading;
            using UnityEngine;
            using MirJan.Helpers;

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
                public int NumberOfThreads = 3;
                #endregion

                protected override void Awake()
                {
                    base.Awake();

                    CreateGraph();

                    PathFindingAgent.RequestPathMethod = RequestPathMethod;
                }

                private void Update()
                {
                    PathFinderManager.Instance.Update();
                }

                #region Path Finding Agent Methods
                void RequestPathMethod(PathRequest pathRequest)
                {
                    PathFinderManager.RequestPath(pathRequest);
                }
                #endregion

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
                    readonly ConcurrentQueue<PathRequest> incompleteJobs;
                    readonly ConcurrentQueue<PathResult> completedJobs;

                    public PathFinderManager()
                    {
                        incompleteJobs = new ConcurrentQueue<PathRequest>();
                        completedJobs = new ConcurrentQueue<PathResult>();
                    }

                    public void Update()
                    {
                        while(completedJobs.Count > 0)
                        {                        
                            if(completedJobs.TryDequeue(out PathResult pathResult))
                            {
                                pathResult.Callback(pathResult.Path, pathResult.IsSuccess);
                            }                         
                        }

                        if(incompleteJobs.Count > 0 && completedJobs.Count < MasterPathFinderManager3D<Type>.Instance.NumberOfThreads)
                        {
                            if(incompleteJobs.TryDequeue(out PathRequest pathRequest))
                            {
                                PathFinder pathFinder = new PathFinder();

                                Thread thread = new Thread(() => pathFinder.FindPath(pathRequest, FinishedProcessingPath));

                                thread.Start();
                            }          
                        }
                    }

                    public static void RequestPath(PathRequest request)
                    {
                        Instance.incompleteJobs.Enqueue(request);
                    }

                    public void FinishedProcessingPath(PathResult result)
                    {
                        completedJobs.Enqueue(result);     
                    }
                }
                #endregion

                #region Path Finder
                public class PathFinder
                {                  
                    readonly GraphSearcher<Node, Type> graphSearcher;

                    public PathFinder()
                    {
                        if(Instance.searchType == SearchType.ASTAR_SEARCH)
                        {
                            graphSearcher = new AStarSearch<Node, Type>(Instance, false);
                        }
                        else if(Instance.searchType == SearchType.DIJKSTRA_SEARCH)
                        {
                            graphSearcher= new DijkstraSearch<Node, Type>(Instance, false);
                        }
                        else if(Instance.searchType == SearchType.GREEDYBESTFIRST_SEARCH)
                        {
                            graphSearcher = new GreedyBestFirstSearch<Node, Type>(Instance, false);
                        }
                        else//if(Instance.searchType == SearchType.FRINGE_SEARCH)
                        {
                            graphSearcher = new FringeSearch<Node, Type>(Instance, false);
                        }
                    }

                    public void FindPath(PathRequest pathRequest, Action<PathResult> callback)
                    {
                        graphSearcher.Initialize(Instance.NodeFromWorldPoint(pathRequest.StartPosition), Instance.NodeFromWorldPoint(pathRequest.TargetPosition));

                        while (graphSearcher.IsRunning) graphSearcher.Step();

                        callback(new PathResult(GetWayPoints(graphSearcher.PathList), graphSearcher.IsSucceeded, pathRequest.Callback));
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

