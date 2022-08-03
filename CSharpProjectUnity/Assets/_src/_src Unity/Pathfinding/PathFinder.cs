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
            public class PathFinder<Type>
            {
                readonly GraphSearcher<Node, Type> graphSearcher;

                readonly PathFinderManager<Type> graph;
                readonly Action<PathResult> callBack;
                readonly PathRequest pathRequest;
              
                public PathFinder(PathFinderManager<Type> graph, PathRequest pathRequest, Action<PathResult> callBack)
                {
                    this.graph = graph;
                    this.pathRequest = pathRequest;
                    this.callBack = callBack;
                    graphSearcher = new AStarSearch<Node, Type>(graph);
                }

                public void FindPath()
                {
                    graphSearcher.Initialize(NodeFromWorldPoint(pathRequest.StartPosition), NodeFromWorldPoint(pathRequest.TargetPosition));

                    if (!graphSearcher.TargetNode.IsWalkable || !graphSearcher.SourceNode.IsWalkable)
                    {
                        graphSearcher.ForceReset();
                        callBack(new PathResult(null, false, pathRequest.Callback));
                        return;
                    }

                    while (graphSearcher.IsRunning) graphSearcher.Step();

                    callBack(new PathResult(GetWayPoints(graphSearcher.PathList), graphSearcher.IsSucceeded, pathRequest.Callback));
                    return;
                }

                Node NodeFromWorldPoint(Vector3 position)
                {
                    Node node;

                    lock (graph)
                    {
                        node = graph.NodeFromWorldPoint(position);
                    }

                    return node;
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

