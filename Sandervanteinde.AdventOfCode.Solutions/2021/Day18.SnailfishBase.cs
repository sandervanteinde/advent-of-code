namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day18
{
    public abstract class SnailfishBase
    {
        public SnailfishPair? Parent { get; set; }
        public abstract bool AttemptExplode(int depth = 1);
        public abstract bool AttemptSplit();
        public abstract long Magnitude();

        public abstract SnailfishBase Clone();

        public void Reduce()
        {
            while (AttemptExplode() || AttemptSplit())
            {
                // no-op
            }
        }

        public static SnailfishBase Parse(string str)
        {
            return ParseSnailfish(str.GetEnumerator());
            static SnailfishBase ParseSnailfish(IEnumerator<char> enumerator)
            {
                enumerator.MoveNext();
                if (enumerator.Current == '[')
                {
                    var pair = new SnailfishPair
                    {
                        Left = ParseSnailfish(enumerator)
                    };
                    enumerator.MoveNext();
                    if (enumerator.Current != ',')
                    {
                        throw new InvalidOperationException("Expected a comma separating the snailfishes.");
                    }
                    pair.Right = ParseSnailfish(enumerator);
                    enumerator.MoveNext();
                    if (enumerator.Current != ']')
                    {
                        throw new InvalidOperationException("Expected closing ']' after parsing snailfish");
                    }
                    return pair;
                }
                if (enumerator.Current == ']')
                {
                    throw new InvalidOperationException("Missing starting bracket");
                }
                return new Snailfish(enumerator.Current - 48);
            }
        }


    }
}
