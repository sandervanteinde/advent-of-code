namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day04
{
    private class BingoItem
    {
        public bool Tagged { get; set; }
        public int Value { get; set; }

        public BingoItem(int value)
        {
            Value = value;
        }
    }
}
