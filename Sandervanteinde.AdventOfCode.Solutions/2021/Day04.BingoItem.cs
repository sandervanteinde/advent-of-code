namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day04
{
    private class BingoItem
    {
        public BingoItem(int value)
        {
            Value = value;
        }

        public bool Tagged { get; set; }
        public int Value { get; }
    }
}
