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

            [Serializable]
            public abstract class SingletonGameObject<T> : BaseSingleton where T : SingletonGameObject<T>
            {
                #region Singleton
                public static T Instance => Lazy.instance;

                private class Lazy
                {
                    static Lazy() { }

                    internal static readonly T instance = GetSingleton();

                    private static T GetSingleton()
                    {
                        BaseSingleton instance = singletons[typeof(T)];
                        return (T)instance;
                    }
                }
                #endregion
            }
        }
    }
}
