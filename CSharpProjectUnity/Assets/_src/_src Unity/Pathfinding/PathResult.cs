namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using UnityEngine;
            using System;
            public struct PathResult
            {
                public Vector3[] Path { get; private set; }
                public bool IsSuccess { get; private set; }
                public Action<Vector3[], bool> CallBack { get; private set; }

                public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback)
                {
                    this.Path = path;
                    this.IsSuccess = success;
                    this.CallBack = callback;
                }
            }
        }
    }
}
