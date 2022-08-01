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
            public class PathFinder<Graph, Type> where Graph : PathFinderManager<Graph, Type>
            {
                readonly GraphSearcher<Node, Type> graphSearcher;
                readonly PathFinderManager<Graph, Type> graph;

                public PathFinder(PathFinderManager<Graph, Type> graph)
                {
                    FactoryMethod<GraphSearcher<Node, Type>>.Register((int)PathFinderManager<Graph, Type>.SearchType.ASTAR_SEARCH, () => new AStarSearch<Node, Type>(graph, false));
                    FactoryMethod<GraphSearcher<Node, Type>>.Register((int)PathFinderManager<Graph, Type>.SearchType.DIJKSTRA_SEARCH, () => new DijkstraSearch<Node, Type>(graph, false));
                    FactoryMethod<GraphSearcher<Node, Type>>.Register((int)PathFinderManager<Graph, Type>.SearchType.GREEDYBESTFIRST_SEARCH, () => new GreedyBestFirstSearch<Node, Type>(graph, false));
                    FactoryMethod<GraphSearcher<Node, Type>>.Register((int)PathFinderManager<Graph, Type>.SearchType.FRINGE_SEARCH, () => new FringeSearch<Node, Type>(graph, false));

                    graphSearcher = FactoryMethod<GraphSearcher<Node, Type>>.Create((int)graph.searchType);

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

