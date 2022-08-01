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
            using UnityEngine.SceneManagement;

            public abstract class PathFinderManager<Graph, Type> : MonoBehaviour, IGraphSearchable<PathFinder<Graph, Type>.Node, Type> where Graph : PathFinderManager<Graph, Type>
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

                public float minimumPathUpdateTime;
                public float pathUpdateThreshold;

                [Header("MultiThreading")]
                [Range(1f, 16f)]
                public int ThreadCount = 3;
                #endregion

                PathRequestManager<Graph, Type> pathRequestManager;

                private void Awake()
                {                  
                    pathRequestManager = new PathRequestManager<Graph, Type>(this);

                    PathFindingAgent.MINIMUM_PATH_UPDATE_TIME = minimumPathUpdateTime;
                    PathFindingAgent.PATH_UPDATE_THRESHOLD = pathUpdateThreshold;

                    CreateGraph();
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
                public abstract PathFinder<Graph, Type>.Node NodeFromWorldPoint(Vector3 worldPosition);
                public abstract void CreateGraph();
                #endregion

                #region Graph Searchable Interface
                public abstract List<PathFinder<Graph, Type>.Node> GetNeighbourNodes(PathFinder<Graph, Type>.Node node);
                public abstract float HeuristicCost(Type valueA, Type valueB);
                public abstract float NodeTraversalCost(Type valueA, Type valueB);
                #endregion
            }
        }
    }
}

