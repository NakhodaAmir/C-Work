namespace MirJan
{
    namespace Unity
    {
        namespace Helpers
        {
            public abstract class SingletonBehaviour<T> : UnityEngine.MonoBehaviour where T : UnityEngine.MonoBehaviour
            {
                private static System.Lazy<T> instance;

                public static T Instance { get { return instance?.Value; } }

                protected virtual void Awake()
                {
                    if (Instance != null)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        instance = new System.Lazy<T>(() => this as T);
                    }
                }

                protected virtual void OnApplicationQuit()
                {
                    instance = null;
                    Destroy(gameObject);
                }
            }
        }
    }
}

