namespace MirJan
{
    namespace Unity
    {
        namespace Editor
        {
            using MirJan.Unity.PathFinding.Graphs;
            using MirJan.Unity.PathFinding;
            using UnityEditor;
            using UnityEngine;
            using MirJan.Unity.Managers;
            using MirJan.Unity.Helpers;
            using System;
            using System.Linq;
            using System.Collections.Generic;

            public class MirjanEditor : Editor
            {
                [MenuItem("MirJan Assets/Path Finding/Square Grid Graph", false, -1)]
                public static void CreateGridGraph()
                {
                    GameObject graph = new GameObject(nameof(SquareGridGraph));
                    graph.AddComponent<SquareGridGraph>();
                }

                [MenuItem("MirJan Assets/Managers/Audio Manager", false, -1)]
                public static void CreateAudioManager()
                {
                    AudioManager audioManager = CreateInstance<AudioManager>();

                    AssetDatabase.CreateAsset(audioManager, AssetDatabase.GenerateUniqueAssetPath("Assets/NewAudioManager.asset"));

                    AssetDatabase.SaveAssets();

                    EditorUtility.FocusProjectWindow();

                    Selection.activeObject = audioManager;
                }
            }

            [CustomEditor(typeof(PathFinderManager<,>), true, isFallback = true)]
            public class PathFinderManagerEditor : Editor
            {
                protected bool isPathFinderManagerEditor;

                protected UnityEngine.Object[] duplicatePathFinderManagers = null;

                protected bool HasDuplicates
                {
                    get { return duplicatePathFinderManagers != null && duplicatePathFinderManagers.Length > 1; }
                }

                protected virtual void OnEnable()
                {
                    isPathFinderManagerEditor = target && IsSubclassOfRawGeneric(typeof(PathFinderManager<,>), target.GetType());

                    if (!isPathFinderManagerEditor)
                    {
                        return;
                    }

                    duplicatePathFinderManagers = FindObjectsOfType(target.GetType());
                }

                public override void OnInspectorGUI()
                {
                    DrawDefaultInspector();

                    BeforePathFinderManagerValidation();

                    DrawPathFinderManagerValidation();
                }

                public static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
                {
                    if (generic == toCheck) return false;

                    while (toCheck != null && toCheck != typeof(object))
                    {
                        var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                        if (generic == cur)
                        {
                            return true;
                        }
                        toCheck = toCheck.BaseType;
                    }

                    return false;
                }

                public virtual void BeforePathFinderManagerValidation() { }

                protected void DrawPathFinderManagerValidation()
                {
                    if (!isPathFinderManagerEditor)
                    {
                        return;
                    }

                    if (HasDuplicates)
                    {
                        EditorGUILayout.HelpBox("There are duplicate Path Finder Manager instances. This can lead to unexpected results.", MessageType.Warning, true);
                        EditorGUILayout.LabelField("Duplicate Path Finder Manager instances:");
                        GUI.enabled = false;
                        for (var i = 0; i < duplicatePathFinderManagers.Length; ++i)
                        {
                            var duplicateSingleton = duplicatePathFinderManagers[i];
                            EditorGUILayout.ObjectField(duplicateSingleton.name, duplicateSingleton, target.GetType(), false);
                        }
                        GUI.enabled = true;
                    }
                }
            }

            [CustomEditor(typeof(AudioManager))]
            public class AudioManagerEditor : SingletonEditor
            {
                public override void BeforeSingletonValidation()
                {
                    AudioManager audioManager = (AudioManager)target;

                    GUILayout.Label("Enum Generation", EditorStyles.boldLabel);

                    if (GUILayout.Button("Generate Audio Types"))
                    {
                        audioManager.GenerateAudioTypes();
                    }

                    if(audioManager.audioTypesTextInfo != "")
                    {
                        EditorGUILayout.LabelField("Enum AUDIOTYPES");
                        EditorGUILayout.HelpBox(audioManager.audioTypesTextInfo, MessageType.None);
                    }     
                }
            }

            [CustomEditor(typeof(BasePersistentSingleton), true, isFallback = true)]
            public class SingletonEditor : Editor
            {
                protected bool isSingletonEditor;

                protected ScriptableObject[] duplicateSingletons = null;

                protected bool HasDuplicates
                {
                    get { return duplicateSingletons != null && duplicateSingletons.Length > 1; }
                }

                protected virtual void OnEnable()
                {
                    isSingletonEditor = target && target.GetType().IsSubclassOf(typeof(BasePersistentSingleton));

                    if (!isSingletonEditor)
                    {
                        return;
                    }

                    duplicateSingletons = GetAllScriptableObjects(target.GetType());
                }

                public override void OnInspectorGUI()
                {
                    DrawDefaultInspector();

                    BeforeSingletonValidation();

                    DrawSingletonValidation();
                }

                public virtual void BeforeSingletonValidation() { }

                protected void DrawSingletonValidation()
                {
                    if (!isSingletonEditor)
                    {
                        return;
                    }

                    if (HasDuplicates)
                    {
                        EditorGUILayout.HelpBox("There are duplicate " + target.GetType().Name + " (Singleton) instances. This can lead to unexpected results.", MessageType.Warning, true);
                        EditorGUILayout.LabelField("Duplicate " + target.GetType().Name + " Singletons:");
                        GUI.enabled = false;
                        for (var i = 0; i < duplicateSingletons.Length; ++i)
                        {
                            var duplicateSingleton = duplicateSingletons[i];
                            EditorGUILayout.ObjectField(duplicateSingleton.name, duplicateSingleton, target.GetType(), false);
                        }
                        GUI.enabled = true;
                    }
                }

                public static ScriptableObject[] GetAllScriptableObjects(Type scriptableObjectType)
                {
                    string[] assets = AssetDatabase.FindAssets("t:" + scriptableObjectType.Name);

                    return assets.Select(id => AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(id))).ToArray();
                }
            }

            //[CustomEditor(typeof(PersistentSingletonBehaviour), true)]
            //public class PersistentSingletonBehaviourEditor : Editor
            //{

            //    private Dictionary<int, Editor> _editors;

            //    private Dictionary<int, bool> _foldouts;

            //    private static GUIStyle _boldLabel;

            //    public static GUIStyle BoldLabel
            //    {
            //        get
            //        {
            //            if (_boldLabel == null)
            //            {
            //                _boldLabel = new GUIStyle("BoldLabel");
            //            }

            //            return _boldLabel;
            //        }
            //    }

            //    public void OnEnable()
            //    {
            //        _editors = new Dictionary<int, Editor>();

            //        _foldouts = new Dictionary<int, bool>();
            //    }

            //    public override void OnInspectorGUI()
            //    {
            //        base.OnInspectorGUI();

            //        EditorGUILayout.LabelField("Singletons", BoldLabel);

            //        int i = 0;

            //        foreach(KeyValuePair<Type, BasePersistentSingleton> keyValuePair in BasePersistentSingleton.singletons)
            //        {
            //            if (!_editors.ContainsKey(i))
            //            {
            //                _editors.Add(i, null);
            //            }

            //            if (!_foldouts.ContainsKey(i))
            //            {
            //                _foldouts.Add(i, true);
            //            }

            //            var singleton = keyValuePair.Value;

            //            _foldouts[i] = EditorGUILayout.InspectorTitlebar(_foldouts[i], singleton);

            //            if (_foldouts[i])
            //            {
            //                var editor = _editors[i];

            //                CreateCachedEditor(singleton, null, ref editor);

            //                _editors[i] = editor;

            //                EditorGUI.indentLevel += 1;
            //                editor.OnInspectorGUI();

            //                EditorGUILayout.Space();

            //                if (AssetDatabase.Contains(singleton))
            //                {
            //                    EditorGUILayout.ObjectField("Reference", singleton, singleton.GetType(), false);
            //                }
            //                else
            //                {
            //                    EditorGUILayout.HelpBox("Runtime generated", MessageType.Info);
            //                }

            //                EditorGUI.indentLevel -= 1;
            //            }
            //        } 
            //    }
            //}
        }
    }
}
