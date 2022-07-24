namespace MirJan
{
    namespace Helpers
    {
        abstract public class Enumeration : System.IComparable
        {
            #region Properties
            public int Id { get; private set; }
            public string Name { get; set; }
            #endregion

            #region Static Variables
            private static int id = 0;
            #endregion

            #region Constructor
            protected Enumeration(string name)
            {
                Name = name;

                Id = id++;
            }
            #endregion

            #region Public Methods
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
            #endregion
        }
    }
}