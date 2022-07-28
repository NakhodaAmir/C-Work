namespace MirJan
{
    namespace Helpers
    {
        using System;
        using System.Collections.Generic;
        public static class Factory<T>
        {
            static readonly Dictionary<int, Func<T>> dictionary = new Dictionary<int, Func<T>>();

            public static T Create(int id)
            {
                if (dictionary.TryGetValue(id, out Func<T> constructor))
                {
                    return constructor();
                }
                else
                {
                    throw new ArgumentException("Type not registered in factory");
                }
            }

            public static void Register(int id, Func<T> constructor)
            {
                dictionary.Add(id, constructor);
            }
        }
    }
}
