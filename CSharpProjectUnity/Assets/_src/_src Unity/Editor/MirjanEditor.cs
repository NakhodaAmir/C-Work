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
            public class MirjanEditor : Editor
            {
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
            }

            [CustomEditor(typeof(AudioManager))]
            public class AudioManagerEditor : Editor
            {
                public override void OnInspectorGUI()
                {
                    DrawDefaultInspector();

                    AudioManager audioManager = (AudioManager)target;

                    GUILayout.Label("Enum Generation", EditorStyles.boldLabel);
                    if(GUILayout.Button("Generate Audio Types"))
                    {
                        audioManager.GenerateAudioTypes();
                    }

                    EditorGUILayout.HelpBox(audioManager.audioTypesTextInfo, MessageType.None);
                }
            }
        }
    }
}
