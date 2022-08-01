namespace MirJan
{
    namespace Unity
    {
        namespace Helpers
        {
            using System;
            using System.Collections;
            using System.Collections.Generic;
            using UnityEngine;
            using UnityEngine.SceneManagement;

            public abstract class BaseSingleton : ScriptableObject
            {
                #region Singleton Container
                protected static readonly Dictionary<Type, BaseSingleton> singletons = new Dictionary<Type, BaseSingleton>();
                #endregion

                #region Before Scene Initialization
                [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
                private static void InitializeSingletons()
                {
                    foreach (KeyValuePair<Type, BaseSingleton> singleton in singletons)
                    {
                        singleton.Value.Initialize();
                    }
                }
                #endregion

                #region Protected & Private Methods
                private void OnEnable()
                {
                    if (singletons.ContainsKey(GetType()))
                    {
                        DestroyImmediate(this);
                    }
                    else
                    {
                        hideFlags = HideFlags.DontUnloadUnusedAsset;
                        singletons.Add(GetType(), this);
                    }
                }

                private void OnDisable()
                {
                    
                }

                protected virtual void Initialize()
                {
                    SingletonBehaviour.Instance.OnApplicationQuitEvent += Terminate;

                    SceneManager.sceneLoaded += OnSceneLoaded;

                    SingletonBehaviour.Instance.StartEvent += OnStart;

                    SingletonBehaviour.Instance.UpdateEvent += OnUpdate;
                    SingletonBehaviour.Instance.FixedUpdateEvent += OnFixedUpdate;
                    SingletonBehaviour.Instance.LateUpdateEvent += OnLateUpdate;

                    SingletonBehaviour.Instance.OnDrawGizmosEvent += OnDrawGizmos;
                }

                protected virtual void Terminate() 
                {
                    singletons.Clear();
                }

                #region UnityEvents
                protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) { }
                protected virtual void OnStart() { }
                protected virtual void OnFixedUpdate() { }
                protected virtual void OnUpdate() { }
                protected virtual void OnLateUpdate() { }
                protected virtual void OnApplicationQuit() { }
                protected virtual void OnDrawGizmos() { }
                #endregion

                #region Coroutine
                protected Coroutine StartCoroutine(IEnumerator routine)
                {
                    return SingletonBehaviour.Instance.StartCoroutine(routine);
                }

                protected void StopCoroutine(Coroutine routine)
                {
                    SingletonBehaviour.Instance.StopCoroutine(routine);
                }

                protected void StopCoroutine(IEnumerator routine)
                {
                    SingletonBehaviour.Instance.StopCoroutine(routine);
                }

                protected void StopAllCoroutines()
                {
                    SingletonBehaviour.Instance.StopAllCoroutines();
                }
                #endregion
                #endregion
            }
        }
    }
}
