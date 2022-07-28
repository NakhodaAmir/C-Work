namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using GenericGraphs.GraphSearch;
            using Helpers;
            using MirJan.Helpers;
            using System.Collections.Generic;
            using UnityEngine;

            public abstract class PathFinderManager<Type> : SingletonBehaviour<PathFinderManager<Type>>, IGraphSearchable<PathFinder<Type>.Node, Type>
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

                PathRequestManager<Type> pathRequestManager;

                protected override void Awake()
                {
                    base.Awake();

                    CreateGraph();

                    pathRequestManager = new PathRequestManager<Type>(this);
                }

                private void Update()
                {
                    pathRequestManager.Update();
                }

                private void LateUpdate()
                {
                    pathRequestManager.LateUpdate();
                }

                #region Graph 3D Interface
                public abstract PathFinder<Type>.Node NodeFromWorldPoint(Vector3 worldPosition);
                public abstract void CreateGraph();
                #endregion

                #region Graph Searchable Interface
                public abstract List<PathFinder<Type>.Node> GetNeighbourNodes(PathFinder<Type>.Node node);
                public abstract float HeuristicCost(Type valueA, Type valueB);
                public abstract float NodeTraversalCost(Type valueA, Type valueB);
                #endregion
            }
        }
    }
}

