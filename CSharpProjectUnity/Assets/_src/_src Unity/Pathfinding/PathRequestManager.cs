

namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using System.Collections.Concurrent;
            using System.Threading;
            using UnityEngine;

            public class PathRequestManager<Type>
            {              
                readonly int maxThreadCount;

                readonly ConcurrentQueue<PathRequest?> pathRequests = new ConcurrentQueue<PathRequest?>();

                readonly PathFinder<Type> pathFinder;

                volatile int activeThreadCount = 0;

                public PathRequestManager(PathFinderManager<Type> graph)
                {
                    maxThreadCount = graph.ThreadCount;

                    PathFindingAgent.RequestPath = EnqueuePathRequest;

                    pathFinder = new PathFinder<Type>(graph);
                }

                public void Update()
                {

                }

                public void LateUpdate()
                {
                    while (pathRequests.Count > 0 && activeThreadCount < maxThreadCount)
                    {
                        pathRequests.TryDequeue(out PathRequest? pathRequest);

                        if (pathRequest != null)
                        {
                            Thread thread = new Thread(() => pathFinder.FindPath((PathRequest)pathRequest, PathResultCallBack));
                            thread.Start();
                            activeThreadCount++;
                        }
                    }
                }

                public void EnqueuePathRequest(PathRequest pathRequest)
                {
                    pathRequests.Enqueue(pathRequest);
                }

                public void PathResultCallBack(PathResult pathResult)
                {
                    pathResult.Callback(pathResult.Path, pathResult.IsSuccess);
                    activeThreadCount--;
                }

            }
        }
    }
}
        
