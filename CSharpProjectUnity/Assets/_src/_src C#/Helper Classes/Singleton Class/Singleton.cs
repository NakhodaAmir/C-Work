namespace MirJan
{
    namespace Helpers
    {
        abstract public class Singleton<T> where T : Singleton<T>
        {
            protected Singleton() { }

            public static T Instance => Lazy.instance;

            private class Lazy
            {
                static Lazy() { }

                internal static readonly T instance = System.Activator.CreateInstance(typeof(T), true) as T;
            }
        }
    }
}

