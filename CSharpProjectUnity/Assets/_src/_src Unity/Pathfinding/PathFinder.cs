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
            public class PathFinder<Graph, Type> where Graph : PathFinderManager<Graph, Type>
            {
                #region Variables
                readonly GraphSearcher<Node, Type> graphSearcher;
                #endregion

                #region Constructor
                public PathFinder()
                {
                    graphSearcher = new FringeSearch<Node, Type>(PathFinderManager<Graph, Type>.Instance);
                }
                #endregion

                #region Public Methods
                public PathResult FindPath(PathRequest pathRequest)
                {
                    graphSearcher.Initialize(NodeFromWorldPoint(pathRequest.StartPosition), NodeFromWorldPoint(pathRequest.TargetPosition));

                    if (!graphSearcher.TargetNode.IsWalkable || !graphSearcher.SourceNode.IsWalkable)
                    {
                        graphSearcher.ForceReset();

                        return new PathResult(null, false, pathRequest.Callback);
                    }

                    while (graphSearcher.IsRunning) graphSearcher.Step();

                    return new PathResult(GetWayPoints(graphSearcher.PathList), graphSearcher.IsSucceeded, pathRequest.Callback);
                }
                #endregion

                #region Private Methods
                Node NodeFromWorldPoint(Vector3 position)
                {
                    return PathFinderManager<Graph, Type>.Instance.NodeFromWorldPoint(position);
                }

                Vector3[] GetWayPoints(List<Node> pathList)
                {
                    return PathFinderManager<Graph, Type>.Instance.GetWayPoints(pathList);
                }
                #endregion

                #region Path Finder Node
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

