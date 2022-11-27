namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day24
{
    public class SimpleInstruction
    {
        public Instruction[] Instruction { get; init; }
        public long Div { get; init; }
        public long Check { get; init; }
        public long Offset { get; init; }
        public int Index { get; init; }
    }
}
