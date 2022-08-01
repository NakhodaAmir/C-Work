namespace MirJan
{
    namespace Helpers
    {
        using System;

        public static class AbstractFactory
        {
            public static T Create<T>(int id)
            {
                return FactoryMethod<T>.Create(id);
            }

            public static void Register<T>(int id, Func<T> constructor)
            {
                FactoryMethod<T>.Register(id, constructor);
            }
        }
    }
}
