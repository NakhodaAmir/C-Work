namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using GenericGraphs.GraphSearch;
            using Helpers;
            using System.Collections.Generic;
            using UnityEngine;

            public abstract class PathFinderManager<Graph, Type> : Singleton<Graph>, IGraphSearchable<PathFinder<Graph, Type>.Node, Type> where Graph : PathFinderManager<Graph, Type>, IGraphSearchable<PathFinder<Graph, Type>.Node, Type>
            {
                #region Public Variables
                [Header("Path Finding Attributes")]
                public float minimumPathUpdateTime;
                public float pathUpdateThreshold;

                [Header("MultiThreading")]
                [Range(1f, 16f)]
                public int ThreadCount = 3;
                #endregion

                #region Unity Methods
                protected override void Awake()
                {                  
                    base.Awake();

                    PathRequestManager<Graph, Type>.Instance.Awake();

                    PathRequest.RequestPath = PathRequestManager<Graph, Type>.Instance.EnqueuePathRequest;

                    PathFindingAgent.MINIMUM_PATH_UPDATE_TIME = minimumPathUpdateTime;
                    PathFindingAgent.PATH_UPDATE_THRESHOLD = pathUpdateThreshold;

                    CreateGraph();
                }

                private void LateUpdate()
                {
                    PathRequestManager<Graph, Type>.Instance.LateUpdate();
                }
                #endregion

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

