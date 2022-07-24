namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using System;
            using UnityEngine;
            public struct PathResult
            {
                public Vector3[] Path { get; private set; }
                public bool IsSuccess { get; private set; }
                public Action<Vector3[], bool> Callback { get; private set; }

                public PathResult(Vector3[] path, bool isSuccess, Action<Vector3[], bool> callback)
                {
                    Path = path;
                    IsSuccess = isSuccess;
                    Callback = callback;
                }
            }
        }
    }
}
