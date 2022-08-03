

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

                readonly ConcurrentQueue<PathFinder<Type>> jobs = new ConcurrentQueue<PathFinder<Type>>();

                volatile int activeThreadCount = 0;

                readonly PathFinderManager<Type> graph;

                public PathRequestManager(PathFinderManager<Type> graph)
                {
                    maxThreadCount = graph.ThreadCount;

                    PathRequest.RequestPath = EnqueuePathRequest;

                    this.graph = graph;
                }

                public void Update()
                {
                    if (jobs.Count > 0 && activeThreadCount < maxThreadCount)
                    {
                        jobs.TryDequeue(out PathFinder<Type> job);

                        if (job != null)
                        {
                            Thread thread = new Thread(job.FindPath);
                            thread.Start();
                            activeThreadCount++;
                        }
                    }
                }

                public void EnqueuePathRequest(PathRequest pathRequest)
                {
                    PathFinder<Type> pathFinder = new PathFinder<Type>(graph, pathRequest, PathResultCallBack);
                    jobs.Enqueue(pathFinder);
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
        
