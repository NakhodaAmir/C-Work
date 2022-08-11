namespace MirJan
{
    namespace Unity
    {
        namespace Managers
        {
            using MirJan.Unity.Helpers;
            using UnityEditor;
            using System.IO;
            using System.Collections.Generic;

            public class Manager<T> : PersistentSingleton<T> where T : PersistentSingleton<T>
            {
                protected void GenerateEnum(List<string> enumValues, int lineToEdit, string path, out string display)
                {
                    string[] lines = File.ReadAllLines(path);

                    string code = "";

                    for (int i = 0; i < enumValues.Count; i++)
                    {
                        code += $"{enumValues[i].ToUpper()}, ";
                    }

                    display = code;

                    lines[lineToEdit] = code;

                    File.WriteAllLines(path, lines);

                    AssetDatabase.Refresh();
                }
            }
        }
    }
}

