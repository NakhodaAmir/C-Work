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
                public static float MINIMUM_PATH_UPDATE_TIME { get; set; }
                public static float PATH_UPDATE_THRESHOLD { get; set; }

                public Transform target;

                Vector3[] wayPoints;

                PathRequest? currentPathRequest;

                #region Debug
                [Header("Debug")]
                public bool displayPathGizmos;
                #endregion

                void Start()
                {
                    StartCoroutine(UpdatePath());
                }

                public void OnPathFound(Vector3[] wayPoints, bool IsSuccess)
                {
                    currentPathRequest?.Dispose();
                    currentPathRequest = null;

                    if (IsSuccess)
                    {
                        this.wayPoints = wayPoints;
                    }
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
            }
        }
    }
}
