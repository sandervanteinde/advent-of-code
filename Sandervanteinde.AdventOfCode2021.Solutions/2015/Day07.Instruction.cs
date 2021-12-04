namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day07
{
    private class Instruction
    {
        public IOperand Operand { get; init; } = null!;
        public string Target { get; init; } = null!;
    }
}
