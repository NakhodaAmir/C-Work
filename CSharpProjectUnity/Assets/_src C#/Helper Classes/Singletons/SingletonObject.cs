namespace MirJan
{
    namespace Helpers
    {
        public sealed class SingletonObject<T> where T : new()
        {
            private static readonly System.Lazy<T> instance = new System.Lazy<T>(() => new T());

            public static T Instance { get { return instance.Value; } }
        }
    }
}