namespace MirJan
{
    namespace Helpers
    {
        using System;

        abstract public class Enumeration : IComparable
        {
            private static int id = 0;

            public int Id { get; private set; }
            public string Name { get; set; }

            protected Enumeration(string name)
            {
                Name = name;

                Id = id++;
            }

            public override string ToString() => Name;

            public override bool Equals(object obj)
            {
                if (!(obj is Enumeration otherValue)) return false;

                var typeMatches = GetType().Equals(otherValue.GetType());
                var idMathches = Id.Equals(otherValue.Id);

                return typeMatches && idMathches;
            }

            public int CompareTo(object obj) => Id.CompareTo(((Enumeration)obj).Id);

            public override int GetHashCode() => Id.GetHashCode();
        }
    }
}