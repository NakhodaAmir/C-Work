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
        }
    }
}
