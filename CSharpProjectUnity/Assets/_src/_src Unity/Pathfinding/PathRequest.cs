namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using System;
            using UnityEngine;
            public struct PathRequest
            {
                public Vector3 StartPosition { get; private set; }
                public Vector3 TargetPosition { get; private set; }
                public Action<Vector3[], bool> Callback { get; private set; }

                public PathRequest(Vector3 startPosition, Vector3 targetPosition, Action<Vector3[], bool> callback)
                {
                    StartPosition = startPosition;
                    TargetPosition = targetPosition;
                    Callback = callback;
                }
            }
        }
    }
}
