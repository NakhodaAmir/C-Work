namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            using System.Collections;
            using UnityEngine;

            public class PathFindingAgent : MonoBehaviour
            {
                public delegate void RequestPath(PathRequest pathRequest);
                public static RequestPath RequestPathMethod { get; set; }

                const float MINIMUM_PATH_UPDATE_TIME = 0.2f;
                const float PATH_UPDATE_THRESHOLD = 0.5f;

                public Transform target;

                Vector3[] wayPoints;

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
                    if (IsSuccess)
                    {
                        this.wayPoints = wayPoints;
                    }
                }

                IEnumerator UpdatePath()
                {
                    if (Time.timeSinceLevelLoad < 0.3f) yield return new WaitForSeconds(0.3f);

                    RequestPathMethod(new PathRequest(transform.position, target.position, OnPathFound));

                    float sqrUpdateThreshold = PATH_UPDATE_THRESHOLD * PATH_UPDATE_THRESHOLD;

                    Vector3 targetPosOld = target.position;

                    while (true)
                    {
                        yield return new WaitForSeconds(MINIMUM_PATH_UPDATE_TIME);

                        if ((target.position - targetPosOld).sqrMagnitude > sqrUpdateThreshold)
                        {
                            RequestPathMethod(new PathRequest(transform.position, target.position, OnPathFound));

                            targetPosOld = target.position;
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
