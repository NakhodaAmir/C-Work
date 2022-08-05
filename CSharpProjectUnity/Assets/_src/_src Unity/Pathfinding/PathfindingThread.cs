namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using System.Collections.Concurrent;
            using System.Collections.Generic;
            using System.Threading;
            public class PathfindingThread<Graph, Type> where Graph : PathFinderManager<Graph, Type>
            {
                #region Constant Variable
                const int IDLE_SLEEP = 5;
                #endregion

                #region Variables
                public Thread Thread;
                public int ThreadNumber;
                public bool Run { get; private set; }

                public Queue<PathRequest?> PathRequestQueue = new Queue<PathRequest?>();
                public PathFinder<Graph, Type> PathFinder;
                #endregion

                #region Constructor
                public PathfindingThread(int number)
                {
                    ThreadNumber = number;
                    PathFinder = new PathFinder<Graph, Type>();
                }
                #endregion

                #region Public Methods
                public void StartThread()
                {
                    if (Run) return;

                    Run = true;
                    Thread = new Thread(new ParameterizedThreadStart(RunThread));
                    Thread.Start(ThreadNumber);
                }

                public void StopThread()
                {
                    Run = false;
                }
                public void RunThread(object n)
                {
                    while (Run)
                    {
                        int count = PathRequestQueue.Count;

                        if(count == 0)
                        {
                            Thread.Sleep(IDLE_SLEEP);
                        }
                        else
                        {
                            lock (PathRequestManager<Graph, Type>.Instance.QueueLock)
                            {
                                PathRequest? pathRequest = PathRequestQueue.Dequeue();

                                if (pathRequest == null) continue;

                                PathFinder.FindPath((PathRequest)pathRequest);
                            }
                        }
                    }
                }
                #endregion
            }
        }
    }
}

