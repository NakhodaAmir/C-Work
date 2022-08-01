namespace MirJan
{
    namespace Helpers
    {
        using System;
        using System.Collections.Generic;
        public static class FactoryMethod<T>
        {
            static readonly Dictionary<int, Func<T>> constructors = new Dictionary<int, Func<T>>();

            public static T Create(int id)
            {
                if (constructors.TryGetValue(id, out Func<T> constructor))
                {
                    return constructor();
                }
                else
                {
                    throw new ArgumentException($"Product type '{typeof(T).Name}' or product Id '{id}' not registered in factory method");
                }
            }

            public static void Register(int id, Func<T> constructor)
            {
                constructors.Add(id, constructor);
            }
        }
    }
}
