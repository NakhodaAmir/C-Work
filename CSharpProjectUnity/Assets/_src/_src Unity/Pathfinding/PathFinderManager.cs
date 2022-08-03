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

            public abstract class PathFinderManager<Type> : MonoBehaviour, IGraphSearchable<PathFinder<Type>.Node, Type>
            {
                #region Public Variables
                [Header("Path Finding Attributes")]
                public float minimumPathUpdateTime;
                public float pathUpdateThreshold;

                [Header("MultiThreading")]
                [Range(1f, 16f)]
                public int ThreadCount = 3;
                #endregion

                PathRequestManager<Type> pathRequestManager;

                private void Awake()
                {                  
                    pathRequestManager = new PathRequestManager<Type>(this);

                    PathFindingAgent.MINIMUM_PATH_UPDATE_TIME = minimumPathUpdateTime;
                    PathFindingAgent.PATH_UPDATE_THRESHOLD = pathUpdateThreshold;

                    CreateGraph();
                }

                private void Update()
                {
                    pathRequestManager.Update();
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

