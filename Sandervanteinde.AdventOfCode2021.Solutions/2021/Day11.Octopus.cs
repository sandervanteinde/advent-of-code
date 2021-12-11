namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day11
{
    public class Octopus
    {
        public int Value { get; private set; }

        public Octopus(int value)
        {
            Value = value;
        }

        public bool Flash()
        {
            if (Value > 9)
            {
                Value = 0;
                return true;
            }
            return false;
        }

        public void Increment()
        {
            Value++;
        }

        public bool IncrementByFlash()
        {
            if (Value != 0)
            {
                Increment();
                return Flash();
            }
            return false;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
