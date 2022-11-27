namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day16
{
    public class BitReader
    {
        private readonly bool[] bits;
        private int index = 0;

        public bool HasMore => index < bits.Length;

        public BitReader(IEnumerable<bool> bits)
        {
            this.bits = bits.ToArray();
        }

        public bool ReadNext()
        {
            return bits[index++];
        }

        public Span<bool> Read(int amount)
        {
            var span = bits.AsSpan(index, amount);
            index += amount;
            return span;
        }

        public long ReadAsNumber(int amountOfBits)
        {
            var span = Read(amountOfBits);
            var result = 0L;
            for (var i = 0; i < amountOfBits; i++)
            {
                result <<= 1;
                if (span[i])
                {
                    result |= 1;
                }
            }
            return result;
        }
    }
}
