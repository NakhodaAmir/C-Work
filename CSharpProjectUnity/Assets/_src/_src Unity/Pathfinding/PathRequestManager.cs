

namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using System;
            using System.Collections.Concurrent;
            using System.Threading;
            public class PathRequestManager<Type>
            {
                readonly int threadCount;

                readonly PathFinder<Type>pathFinder;

                readonly ConcurrentQueue<PathRequest?> pathRequests = new ConcurrentQueue<PathRequest?>();
                readonly ConcurrentQueue<PathResult?> pathResults = new ConcurrentQueue<PathResult?>();

                public PathRequestManager(PathFinderManager<Type> graph)
                {
                    threadCount = graph.ThreadCount;

                    pathFinder = new PathFinder<Type>(graph);

                    PathFindingAgent.RequestPath = EnqueuePathRequest;
                }

                public void Update()
                {
                    while (pathResults.Count > 0)
                    {
                        pathResults.TryDequeue(out PathResult? pathResult);

                        if (pathResult != null)
                        {
                            pathResult?.Callback(pathResult?.Path, (bool)pathResult?.IsSuccess);
                        }
                    }
                }

                public void LateUpdate()
                {
                    while (pathRequests.Count > 0 && pathResults.Count < threadCount)
                    {
                        pathRequests.TryDequeue(out PathRequest? pathRequest);

                        if (pathRequest != null)
                        {
                            Thread thread = new Thread(() => pathFinder.FindPath((PathRequest)pathRequest, EnqueuePathResult));
                            thread.Start();
                        }
                    }
                }

                public void EnqueuePathRequest(PathRequest pathRequest)
                {
                    pathRequests.Enqueue(pathRequest);
                }

                public void EnqueuePathResult(PathResult pathResult)
                {
                    pathResults.Enqueue(pathResult);
                }

            }
        }
    }
}
        
