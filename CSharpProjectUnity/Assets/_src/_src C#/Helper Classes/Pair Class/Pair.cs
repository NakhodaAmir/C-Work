namespace MirJan
{
    namespace Helpers
    {
        using System;
        using System.Collections.Generic;

        public sealed class Pair<FirstValue, SecondValue> : IEquatable<Pair<FirstValue, SecondValue>>
        {
            private readonly FirstValue firstValue;
            private readonly SecondValue secondValue;

            public Pair(FirstValue firstValue, SecondValue secondValue)
            {
                this.firstValue = firstValue;
                this.secondValue = secondValue;
            }

            public FirstValue GetFirstValue => firstValue;
            public SecondValue GetSecondValue => secondValue;

            public bool Equals(Pair<FirstValue, SecondValue> other)
            {
                if (other == null) return false;

                return EqualityComparer<FirstValue>.Default.Equals(firstValue, other.firstValue) && EqualityComparer<SecondValue>.Default.Equals(secondValue, other.secondValue);
            }

            public override bool Equals(object obj)
            {
                return base.Equals(obj as Pair<FirstValue, SecondValue>);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(firstValue.GetHashCode(), secondValue.GetHashCode());
            }

            public override string ToString()
            {
                return base.ToString();
            }
        }
    }
}
