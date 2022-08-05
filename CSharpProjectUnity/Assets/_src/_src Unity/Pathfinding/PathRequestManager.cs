namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using System.Collections.Concurrent;
            using System.Collections.Generic;
            using System.Threading;

            public class PathRequestManager<Graph, Type> where Graph : PathFinderManager<Graph, Type>
            {
                #region Singleton
                private PathRequestManager() { }

                public static PathRequestManager<Graph, Type> Instance => Lazy.instance;

                private class Lazy
                {
                    static Lazy() { }

                    internal readonly static PathRequestManager<Graph, Type> instance = new PathRequestManager<Graph, Type>();
                }
                #endregion

                #region Variables
                PathfindingThread<Graph, Type>[] threads;

                public object QueueLock = new object();
                readonly Queue<PathRequest> pendingPathRequests = new Queue<PathRequest>();
                #endregion

                #region Public Methods
                public void Awake()
                {
                    CreateThreads(PathFinderManager<Graph, Type>.Instance.ThreadCount);
                    StartThreads();
                }


                public void LateUpdate()
                {
                    if (threads == null || threads.Length == 0) return;

                    lock (QueueLock)
                    {

                        while (pendingPathRequests.Count > 0)
                        {
                            PathRequest pathRequest = pendingPathRequests.Dequeue();

                            int lowest = int.MaxValue;
                            PathfindingThread<Graph, Type> t = null;

                            foreach (var thread in threads)
                            {
                                if (thread.PathRequestQueue.Count < lowest)
                                {
                                    lowest = thread.PathRequestQueue.Count;
                                    t = thread;
                                }
                            }

                            t.PathRequestQueue.Enqueue(pathRequest);
                        }
                    }
                }

                public void EnqueuePathRequest(PathRequest pathRequest)
                {
                    lock (QueueLock)
                    {
                        pendingPathRequests.Enqueue(pathRequest);
                    }
                }

                public void CreateThreads(int number)
                {
                    if (threads != null) return;

                    threads = new PathfindingThread<Graph, Type>[number];

                    for (int i = 0; i < number; i++)
                    {
                        threads[i] = new PathfindingThread<Graph, Type>(i);
                    }
                }

                public void StartThreads()
                {
                    if (threads == null) return;

                    for (int i = 0; i < threads.Length; i++)
                    {
                        var t = threads[i];
                        t.StartThread();
                    }
                }

                public void StopThreads()
                {
                    if (threads == null) return;

                    for (int i = 0; i < threads.Length; i++)
                    {
                        var t = threads[i];
                        t.StopThread();
                    }
                }

                public void OnApplicationQuit()
                {
                    if (threads != null) StopThreads();
                }
                #endregion
            }
        }
    }
}
        
