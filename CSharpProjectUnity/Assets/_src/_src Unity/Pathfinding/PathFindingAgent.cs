namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using System;
            using System.Collections;
            using UnityEngine;

            public class PathFindingAgent : MonoBehaviour
            {
                #region Static Variables
                public static float MINIMUM_PATH_UPDATE_TIME { get; set; }
                public static float PATH_UPDATE_THRESHOLD { get; set; }
                #endregion

                #region Variables
                public Transform target;

                Vector3[] wayPoints;

                PathRequest? currentPathRequest;

                bool isFindingPath;
                #endregion

                #region Properties
                public Vector3[] WayPoints { get { return wayPoints; } }
                public bool IsPathSuccess { get; private set; }
                #endregion

                #region Debug
                [Header("Debug")]
                public bool displayPathGizmos;
                #endregion

                #region Public Methods
                public void FindPath()
                {
                    if (isFindingPath) return;

                    isFindingPath = true;
                    StartCoroutine("UpdatePath");
                }

                public void StopFindingPath()
                {
                    if (!isFindingPath) return;

                    isFindingPath = false;
                    StopCoroutine("UpdatePath");
                }
                #endregion

                #region Protected and Private Methods
                protected virtual void OnPathFound() { }
                protected virtual void OnPathFailed() { }

                void OnPathFound(Vector3[] wayPoints, bool IsSuccess)
                {
                    currentPathRequest?.Dispose();
                    currentPathRequest = null;

                    IsPathSuccess = IsSuccess;

                    if (!IsSuccess)
                    {
                        OnPathFailed();
                        return;
                    }

                    this.wayPoints = wayPoints;
                    OnPathFound();
                }

                IEnumerator UpdatePath()
                {
                    if (Time.timeSinceLevelLoad < 0.3f) yield return new WaitForSeconds(0.3f);

                    currentPathRequest = PathRequest.Create(transform.position, target.position, OnPathFound);

                    float sqrUpdateThreshold = PATH_UPDATE_THRESHOLD * PATH_UPDATE_THRESHOLD;

                    Vector3 targetPosOld = target.position;

                    while (true)
                    {
                        yield return new WaitForSeconds(MINIMUM_PATH_UPDATE_TIME);

                        if (currentPathRequest == null)
                        {
                            if ((target.position - targetPosOld).sqrMagnitude > sqrUpdateThreshold)
                            {
                                currentPathRequest = PathRequest.Create(transform.position, target.position, OnPathFound);

                                targetPosOld = target.position;
                            }
                        }
                    }
                }
                #endregion

                #region Debug
                public void OnDrawGizmos()
                {
                    if(wayPoints != null && displayPathGizmos)
                    {
                        Gizmos.color = Color.black;

                        for(int i = 0; i < wayPoints.Length; i++)
                        {
                            Gizmos.DrawCube(wayPoints[i], new Vector3(0.5f, 0.5f, 0.5f));


                            if((i + 1) < wayPoints.Length)
                            {
                                Gizmos.DrawLine(wayPoints[i], wayPoints[i + 1]);
                            }                 
                        }
                    }
                }
                #endregion
            }
        }
    }
}
