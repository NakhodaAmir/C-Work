
namespace MirJan
{
    namespace Helpers
    {
        using System;
        using System.Reflection;

        /// <summary>
        /// <b>REMINEDR: ALWAYS ADD A PRIVATE CONSTRUCTOR WHEN INHERITING THIS CLASS.</b>
        /// <b><br>SINGLETONS CANNOT BE INITIALIZED.</br></b>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        abstract public class Singleton<T> where T : Singleton<T>
        {
            protected Singleton() { }

            public static T Instance => Lazy.instance;

            private class Lazy
            {
                static Lazy() { }

                internal static readonly T instance = CreateInstance();

                private static T CreateInstance()
                {
                    ConstructorInfo constructorInfo = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, Type.DefaultBinder, Type.EmptyTypes, null);

                    if (constructorInfo != null)
                    {
                        return constructorInfo.Invoke(null) as T;
                    }
                    else
                    {
                        throw new Exception($"{typeof(T).Name} must have a private parameterless constructor");
                    }
                }
            }
        }
    }
}

