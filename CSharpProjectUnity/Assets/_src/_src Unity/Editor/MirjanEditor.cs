namespace MirJan
{
    namespace Unity
    {
        namespace Editor
        {
            using MirJan.Unity.PathFinding.Graphs;
            using UnityEditor;
            using UnityEngine;

            public class MirjanEditor : Editor
            {
                [MenuItem("MirJan Assets/PathFinding/Graphs/GridGraph", false, -1)]
                public static void CreateGridGraph()
                {
                    var gameobjects = FindObjectsOfType<GridGraph>();

                    for(int i = 0; i < gameobjects.Length; i++)
                    {
                        DestroyImmediate(gameobjects[i].gameObject);
                    }

                    GameObject graph = new GameObject("Path Finding Manager (Grid Graph)");
                    graph.AddComponent<GridGraph>();
                }
            }
        }
    }
}
