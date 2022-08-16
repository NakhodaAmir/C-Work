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
                public static T Instance { get; private set; }

                protected override void CreateInstance()
                {
                    if (Instance != null)
                    {
                        return;
                    }

                    Instance = GetAndInitialize<T>();
                }
                #endregion
            }
        }
    }
}
