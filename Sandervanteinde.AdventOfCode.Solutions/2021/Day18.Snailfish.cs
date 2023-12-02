namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day18
{
    public class Snailfish : SnailfishBase
    {
        public Snailfish(int value)
        {
            Value = value;
        }

        public int Value { get; set; }

        public override bool AttemptExplode(int depth)
        {
            return false;
        }

        public override long Magnitude()
        {
            return Value;
        }

        public override SnailfishBase Clone()
        {
            return new Snailfish(Value);
        }

        public override bool AttemptSplit()
        {
            if (Value < 10)
            {
                return false;
            }

            if (Parent is null)
            {
                throw new InvalidOperationException("Parent was not a snail fish pair, which is not possible");
            }

            var left = Value / 2;
            var right = Value - left;

            var newSnailfish = new SnailfishPair { Left = new Snailfish(left), Right = new Snailfish(right) };

            if (Parent.Left == this)
            {
                Parent.Left = newSnailfish;
                return true;
            }

            if (Parent.Right == this)
            {
                Parent.Right = newSnailfish;
                return true;
            }

            throw new InvalidOperationException("Parent was not correctly set.");
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
