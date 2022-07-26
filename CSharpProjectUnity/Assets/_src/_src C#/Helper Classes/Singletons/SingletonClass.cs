namespace MirJan
{
    namespace Helpers
    {
        public abstract class SingletonClass<T> where T : SingletonClass<T>
        {
            private static readonly System.Lazy<T> instance = new System.Lazy<T>(() => System.Activator.CreateInstance(typeof(T), true) as T);

            public static T Instance { get { return instance.Value; } }

            protected SingletonClass() 
            {

            }
        }
    }
}