namespace MirJan
{
    namespace Unity
    {
        namespace Helpers
        {
            public abstract class SingletonBehaviour<T> : UnityEngine.MonoBehaviour where T : UnityEngine.MonoBehaviour
            {
                public static T Instance => Lazy.instance;

                private class Lazy
                {
                    static Lazy() { }

                    internal static readonly T instance = CreateOrFindInstance();
                    private static T CreateOrFindInstance()
                    {
                        T instance = FindObjectOfType<T>();

                        if (instance == null)
                        {
                            UnityEngine.GameObject gameObject = new UnityEngine.GameObject() { name = typeof(T).Name };

                            instance = gameObject.AddComponent<T>();
                        }

                        return instance;
                    }
                }

                protected virtual void Awake()
                {
                    if (Instance != null && Instance != this)
                    {
                        Destroy(gameObject);
                    }
                }

                protected virtual void OnApplicationQuit()
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}

