namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using GenericGraphs.GraphSearch;
            using GenericGraphs.GraphSearch.DynamicGraphSearch;
            using System;
            using System.Collections.Generic;
            using UnityEngine;
            using MirJan.Helpers;
            public class PathFinder<Type>
            {
                readonly GraphSearcher<Node, Type> graphSearcher;
                readonly PathFinderManager<Type> graph;

                public PathFinder(PathFinderManager<Type> graph)
                {
                    Factory<GraphSearcher<Node, Type>>.Register((int)PathFinderManager<Type>.SearchType.ASTAR_SEARCH, () => new AStarSearch<Node, Type>(graph, false));
                    Factory<GraphSearcher<Node, Type>>.Register((int)PathFinderManager<Type>.SearchType.DIJKSTRA_SEARCH, () => new DijkstraSearch<Node, Type>(graph, false));
                    Factory<GraphSearcher<Node, Type>>.Register((int)PathFinderManager<Type>.SearchType.GREEDYBESTFIRST_SEARCH, () => new GreedyBestFirstSearch<Node, Type>(graph, false));
                    Factory<GraphSearcher<Node, Type>>.Register((int)PathFinderManager<Type>.SearchType.FRINGE_SEARCH, () => new FringeSearch<Node, Type>(graph, false));

                    graphSearcher = Factory<GraphSearcher<Node, Type>>.Create((int)graph.searchType);

                    this.graph = graph;
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
            }
        }
    }
}

