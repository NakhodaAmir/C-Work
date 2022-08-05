namespace MirJan
{
    namespace Unity
    {
        namespace Helpers
        {
            using System;
            using UnityEngine;
            public class PersistentSingletonBehaviour : MonoBehaviour
            {
                #region Singleton
                public static PersistentSingletonBehaviour Instance => Lazy.instance;

                private class Lazy
                {
                    static Lazy() { }

                    internal static readonly PersistentSingletonBehaviour instance = CreateInstance();
                    
                    private static PersistentSingletonBehaviour CreateInstance()
                    {
                        PersistentSingletonBehaviour instance = FindObjectOfType<PersistentSingletonBehaviour>();

                        if(instance == null)
                        {
                            GameObject gameObject = new GameObject("Singleton Behaviour");
                            instance = gameObject.AddComponent<PersistentSingletonBehaviour>();
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

                #region Unity Events
                private void Awake()
                {
                    DontDestroyOnLoad(gameObject);
                }

                private void Start()
                {
                    StartEvent();
                }

                
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

