namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using System;
            using System.Collections.Generic;
            using UnityEngine;
            public struct PathRequest
            {
                private static Queue<PathRequest> Pooled = new Queue<PathRequest>();

                public static Action<PathRequest> RequestPath;

                public Vector3 StartPosition { get; private set; }
                public Vector3 TargetPosition { get; private set; }
                public Action<Vector3[], bool> Callback { get; private set; }

                private PathRequest(Vector3 startPosition, Vector3 targetPosition, Action<Vector3[], bool> callback)
                {
                    StartPosition = startPosition;
                    TargetPosition = targetPosition;
                    Callback = callback;
                }

                public static PathRequest Create(Vector3 startPosition, Vector3 targetPosition, Action<Vector3[], bool> callback)
                {
                    PathRequest r;

                    if(Pooled.Count > 0)
                    {
                        r = Pooled.Dequeue();
                        r.StartPosition = startPosition;
                        r.TargetPosition = targetPosition;
                        r.Callback = callback;
                    }
                    else
                    {
                        r = new PathRequest(startPosition, targetPosition, callback);
                    }

                    RequestPath(r);

                    return r;
                }

                public void Dispose()
                {
                    if (!Pooled.Contains(this))
                    {
                        Pooled.Enqueue(this);
                    }
                }
            }
        }
    }
}
