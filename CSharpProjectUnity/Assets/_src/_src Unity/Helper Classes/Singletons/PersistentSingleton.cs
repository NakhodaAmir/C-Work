namespace MirJan
{
    namespace Unity
    {
        namespace Helpers
        {
            using System;

            [Serializable]
            public abstract class PersistentSingleton<T> : BasePersistentSingleton where T : PersistentSingleton<T>
            {
                #region Singleton
                public static T Instance => Lazy.instance;

                private class Lazy
                {
                    static Lazy() { }

                    internal static readonly T instance = GetSingleton();

                    private static T GetSingleton()
                    {
                        BasePersistentSingleton instance = singletons[typeof(T)];
                        return (T)instance;
                    }
                }
                #endregion
            }
        }
    }
}
