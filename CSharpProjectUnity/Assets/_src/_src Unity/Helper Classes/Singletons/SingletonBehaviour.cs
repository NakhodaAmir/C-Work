namespace MirJan
{
    namespace Unity
    {
        namespace Helpers
        {
            using System;
            using UnityEngine;
            public class SingletonBehaviour : MonoBehaviour
            {
                #region Singleton
                public static SingletonBehaviour Instance => Lazy.instance;

                private class Lazy
                {
                    static Lazy() { }

                    internal static readonly SingletonBehaviour instance = CreateInstance();
                    
                    private static SingletonBehaviour CreateInstance()
                    {
                        SingletonBehaviour instance = FindObjectOfType<SingletonBehaviour>();

                        if(instance == null)
                        {
                            GameObject gameObject = new GameObject("Singleton Behaviour");
                            instance = gameObject.AddComponent<SingletonBehaviour>();
                        }

                        return instance;
                    }
                }
                #endregion

                #region Singleton Events
                public event Action StartEvent = () => { };

                public event Action FixedUpdateEvent = () => { };
                public event Action UpdateEvent = () => { };
                public event Action LateUpdateEvent = () => { };

                public event Action OnApplicationQuitEvent = () => { };

                public event Action OnDrawGizmosEvent = () => { };
                #endregion

                private void Awake()
                {
                    DontDestroyOnLoad(gameObject);
                }

                private void Start()
                {
                    StartEvent();
                }

                #region Unity Events
                private void FixedUpdate()
                {
                    FixedUpdateEvent();
                }

                private void Update()
                {
                    UpdateEvent();
                }

                private void LateUpdate()
                {
                    LateUpdateEvent();
                }

                private void OnApplicationQuit()
                {
                    if (Instance != null)
                    {
                        Destroy(this);
                    }
                }

                private void OnDrawGizmos()
                {
                    OnDrawGizmosEvent();
                }
                #endregion
            }
        }
    }
}

