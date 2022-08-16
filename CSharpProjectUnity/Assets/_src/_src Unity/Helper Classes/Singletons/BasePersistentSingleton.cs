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

            public abstract class BasePersistentSingleton : ScriptableObject
            {
                #region Singleton Container
                public static readonly Dictionary<Type, BasePersistentSingleton> singletons = new Dictionary<Type, BasePersistentSingleton>();

                protected static T GetAndInitialize<T>() where T : BasePersistentSingleton
                {
                    var instance = (T)singletons[typeof(T)];

                    instance.Initialize();

                    return instance;
                }
                #endregion

                    #region Before Scene Initialization
#if UNITY_EDITOR
                [UnityEditor.InitializeOnLoadMethod]
                private static void LoadPreloadedAssetsInEditor()
                {
                    UnityEditor.PlayerSettings.GetPreloadedAssets();
                }
#endif

                [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
                private static void InitializeSingletons()
                {
                    foreach (KeyValuePair<Type, BasePersistentSingleton> singleton in singletons)
                    {
                        singleton.Value.CreateInstance();
                    }
                }
                #endregion

                #region Protected & Private Methods

                protected abstract void CreateInstance();
                protected virtual void OnEnable()
                {
#if UNITY_EDITOR
                    if (UnityEditor.EditorApplication.isPlaying)
                    {
                        return;
                    }
#endif
                    this.AddToPreloadedAssets();

                    if (!singletons.ContainsKey(GetType()))
                    {
                        singletons.Add(GetType(), this);
                    }         
                }

                protected virtual void OnDisable()
                {
#if UNITY_EDITOR
                    if (UnityEditor.EditorApplication.isPlaying)
                    {
                        return;
                    }
#endif
                    PersistentSingletonHelper.RemoveEmptyPreloadedAssets();

                    if (singletons.ContainsKey(GetType()))
                    {
                        singletons.Remove(GetType());
                    }
                }

                protected virtual void Initialize()
                {
                    PersistentSingletonBehaviour.Instance.OnApplicationQuitEvent += Terminate;

                    SceneManager.sceneLoaded += OnSceneLoaded;

                    PersistentSingletonBehaviour.Instance.StartEvent += OnStart;

                    PersistentSingletonBehaviour.Instance.UpdateEvent += OnUpdate;
                    PersistentSingletonBehaviour.Instance.FixedUpdateEvent += OnFixedUpdate;
                    PersistentSingletonBehaviour.Instance.LateUpdateEvent += OnLateUpdate;

                    PersistentSingletonBehaviour.Instance.OnDrawGizmosEvent += OnDrawGizmos;
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

                public T AddComponent<T>() where T : Component
                {
                    return PersistentSingletonBehaviour.Instance.gameObject.AddComponent<T>();
                }
                #endregion

                #region Coroutine
                protected Coroutine StartCoroutine(IEnumerator routine)
                {
                    return PersistentSingletonBehaviour.Instance.StartCoroutine(routine);
                }

                protected void StopCoroutine(Coroutine routine)
                {
                    PersistentSingletonBehaviour.Instance.StopCoroutine(routine);
                }

                protected void StopCoroutine(IEnumerator routine)
                {
                    PersistentSingletonBehaviour.Instance.StopCoroutine(routine);
                }

                protected void StopAllCoroutines()
                {
                    PersistentSingletonBehaviour.Instance.StopAllCoroutines();
                }
                #endregion
                #endregion
            }
        }
    }
}
