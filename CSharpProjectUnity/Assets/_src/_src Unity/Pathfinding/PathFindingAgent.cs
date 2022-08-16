namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using System;
            using System.Collections;
            using UnityEngine;
            using MirJan.Unity.Helpers;

            public class PathFindingAgent : MonoBehaviour
            {
                #region Static Variables
                public static float MINIMUM_PATH_UPDATE_TIME { get; set; }
                public static float PATH_UPDATE_THRESHOLD { get; set; }
                #endregion

                #region Variables
                public Transform target;

                Vector3 targetPosOld;

                Vector3[] wayPoints;

                PathRequest currentPathRequest;

                float sqrUpdateThreshold;
                #endregion

                #region Debug
                [Header("Debug")]
                public bool displayPathGizmos;
                #endregion

                #region Protected and Private Methods
                protected virtual void Awake()
                {
                    sqrUpdateThreshold = PATH_UPDATE_THRESHOLD * PATH_UPDATE_THRESHOLD;
                }

                protected virtual void Start()
                {
                    StartCoroutine(UpdatePath());
                }

                protected virtual void Update()
                {

                }

                IEnumerator UpdatePath()
                {
                    if (Time.timeSinceLevelLoad < 0.3f) yield return CoroutineHelper.WaitForSeconds(0.3f);

                    currentPathRequest = PathRequest.Create(transform.position, target.position, OnPathFound);

                    targetPosOld = target.position;

                    while (true)
                    {
                        yield return CoroutineHelper.WaitForSeconds(MINIMUM_PATH_UPDATE_TIME);

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

                void OnPathFound(Vector3[] wayPoints, bool IsSuccess)
                {
                    currentPathRequest.Dispose();
                    currentPathRequest = null;

                    if (!IsSuccess) return;

                    this.wayPoints = wayPoints;
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
