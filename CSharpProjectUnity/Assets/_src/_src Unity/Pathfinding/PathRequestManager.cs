namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using System;
            using System.Collections.Concurrent;
            using System.Collections.Generic;
            using System.Text;
            using System.Threading;
            using UnityEngine;

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
                public object CallBackLock = new object();
                readonly List<PathRequest> pendingPathRequests = new List<PathRequest>();
                readonly List<PathResult> pendingPathResults = new List<PathResult>();

                private StringBuilder str = new StringBuilder();
                #endregion

                #region Public Methods
                public void Awake()
                {
                    PathRequest.RequestPath = EnqueuePathRequest;

                    CreateThreads(PathFinderManager<Graph, Type>.Instance.ThreadCount);
                    StartThreads();
                }

                public void Update()
                {
                    lock (CallBackLock)
                    {
                        foreach (PathResult pathResult in pendingPathResults)
                        {
                            if (pathResult.CallBack != null)
                            {
                                pathResult.CallBack(pathResult.Path, pathResult.IsSuccess);
                            }
                        }

                        pendingPathResults.Clear();
                    }
                }

                public void LateUpdate()
                {
                    if (threads == null || threads.Length == 0) return;
#if UNITY_EDITOR
                    str.Append("Requests this frame: ");
                    str.Append(pendingPathRequests.Count);
                    str.AppendLine();
                    str.Append("Pending returns: ");
                    str.Append(pendingPathResults.Count);
                    str.AppendLine();
                    for (int i = 0; i < threads.Length; i++)
                    {
                        var t = threads[i];
                        int count = t.PathRequestQueue.Count;

                        str.Append("Thread #");
                        str.Append(i);
                        str.Append(": ");
                        str.AppendLine();
                        str.Append("  -");
                        str.Append(count);
                        str.Append(" pending requests.");
                        str.AppendLine();
                    }

                    PathFinderManager<Graph, Type>.Instance.info = str.ToString();
                    str.Length = 0;
#endif
                    lock (QueueLock)
                    {
                        foreach (PathRequest pathRequest in pendingPathRequests)
                        {
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

                        pendingPathRequests.Clear();
                    }
                }

                public void EnqueuePathRequest(PathRequest pathRequest)
                {
                    if (Instance == null) return;

                    if (pathRequest == null) return;

                    if (pendingPathRequests.Contains(pathRequest)) return;

                    lock (QueueLock)
                    {
                        pendingPathRequests.Add(pathRequest);
                    }
                }

                public void EnqueuePathResult(PathResult pathResult)
                {
                    lock (CallBackLock)
                    {
                        pendingPathResults.Add(pathResult);
                    }
                }

                public void CreateThreads(int number)
                {
                    if (threads != null) return;

                    threads = new PathfindingThread<Graph, Type>[number];

                    for (int i = 0; i < number; i++)
                    {
                        threads[i] = new PathfindingThread<Graph, Type>(this, i);
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
        
