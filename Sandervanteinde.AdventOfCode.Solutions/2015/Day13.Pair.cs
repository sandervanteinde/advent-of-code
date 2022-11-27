using System.Diagnostics.CodeAnalysis;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day13
{
    public struct Pair
    {
        public string PersonOne { get; set; }
        public string PersonTwo { get; set; }

        public Pair(string personOne, string personTwo)
        {
            PersonOne = personOne;
            PersonTwo = personTwo;
        }

        public class Comparer : IEqualityComparer<Pair>
        {
            public bool Equals(Pair x, Pair y)
            {
                return (x.PersonOne == y.PersonOne && x.PersonTwo == y.PersonTwo)
                    || (x.PersonOne == y.PersonTwo && x.PersonTwo == y.PersonOne);
            }

            public int GetHashCode([DisallowNull] Pair obj)
            {
                return obj.PersonOne.CompareTo(obj.PersonTwo) > 0
                    ? HashCode.Combine(obj.PersonOne, obj.PersonTwo)
                    : HashCode.Combine(obj.PersonTwo, obj.PersonOne);
            }
        }

        public override string ToString()
        {
            return PersonOne.CompareTo(PersonTwo) < 0
                ? $"{PersonOne} / {PersonTwo}"
                : $"{PersonTwo} / {PersonOne}";
        }
    }
}
