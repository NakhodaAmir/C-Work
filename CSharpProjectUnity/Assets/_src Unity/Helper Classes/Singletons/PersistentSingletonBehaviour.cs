namespace MirJan
{
    namespace Unity
    {
        namespace Helpers
        {
            public abstract class PersistentSingletonBehaviour<T> : SingletonBehaviour<T> where T : UnityEngine.MonoBehaviour
            {
                protected override void Awake()
                {
                    base.Awake();
                    DontDestroyOnLoad(gameObject);
                }
            }
        }
    }
}
