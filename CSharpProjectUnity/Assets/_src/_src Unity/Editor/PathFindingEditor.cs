namespace MirJan
{
    namespace Unity
    {
        namespace Editor
        {
            using UnityEditor;
            using UnityEngine;

            public class PathFindingEditor : Editor
            {
                [MenuItem("PathFinding/Graphs/GridGraph3D", false, -1)]
                public static void GridGraph3D()
                {
                    GameObject prefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>("Assets/_src/_src Unity/Prefabs/MasterPathFindingManager.prefab"));

                    prefab.name = "MasterPathFindingManager";
                }
            }
        }
    }
}
