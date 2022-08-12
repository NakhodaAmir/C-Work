namespace MirJan
{
    namespace Unity
    {
        namespace Editor
        {
            using MirJan.Unity.PathFinding.Graphs;
            using UnityEditor;
            using UnityEngine;
            using MirJan.Unity.Managers;
            using MirJan.Unity.Helpers;
            public class MirjanEditor : Editor
            {
                static bool isAudioManagerCreated = false;

                [MenuItem("MirJan Assets/Path Finding/Square Grid Graph", false, -1)]
                public static void CreateGridGraph()
                {
                    var gameobjects = FindObjectsOfType<SquareGridGraph>();

                    for(int i = 0; i < gameobjects.Length; i++)
                    {
                        DestroyImmediate(gameobjects[i].gameObject);
                    }

                    GameObject graph = new GameObject(nameof(SquareGridGraph));
                    graph.AddComponent<SquareGridGraph>();
                }

                [MenuItem("MirJan Assets/Managers/Audio Manager", false, -1)]
                public static void CreateAudioManager()
                {
                    AudioManager audioManager = CreateInstance<AudioManager>();

                    if(!isAudioManagerCreated)
                    {
                        AssetDatabase.CreateAsset(audioManager, "Assets/NewAudioManager.asset");

                        isAudioManagerCreated = true;

                        AssetDatabase.SaveAssets();

                        EditorUtility.FocusProjectWindow();

                        Selection.activeObject = audioManager;
                    }
                    else
                    {
                        Debug.LogError("Cant create more than 1 audio manager");
                    } 
                }
            }

            [CustomEditor(typeof(AudioManager))]
            public class AudioManagerEditor : Editor
            {
                public override void OnInspectorGUI()
                {
                    DrawDefaultInspector();

                    AudioManager audioManager = (AudioManager)target;

                    GUILayout.Label("Enum Generation", EditorStyles.boldLabel);

                    if (GUILayout.Button("Generate Audio Types"))
                    {
                        audioManager.GenerateAudioTypes();
                    }

                    EditorGUILayout.HelpBox(audioManager.audioTypesTextInfo, MessageType.None);
                }
            }
        }
    }
}
