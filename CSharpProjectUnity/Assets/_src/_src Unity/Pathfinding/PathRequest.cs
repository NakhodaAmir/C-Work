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
                #region Static Variables & Methods
                private static readonly Queue<PathRequest> Pooled = new Queue<PathRequest>();

                public static Action<PathRequest> RequestPath;
                #endregion

                #region Properties
                public Vector3 StartPosition { get; private set; }
                public Vector3 TargetPosition { get; private set; }
                public Action<Vector3[], bool> Callback { get; private set; }
                #endregion

                #region Constructor
                private PathRequest(Vector3 startPosition, Vector3 targetPosition, Action<Vector3[], bool> callback)
                {
                    StartPosition = startPosition;
                    TargetPosition = targetPosition;
                    Callback = callback;
                }
                #endregion

                #region Static Methods
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
                #endregion

                #region Public Method
                public void Dispose()
                {
                    if (!Pooled.Contains(this))
                    {
                        Pooled.Enqueue(this);
                    }
                }
                #endregion
            }
        }
    }
}
