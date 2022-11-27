namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day23
{
    private class Computer
    {
        public int RegisterA { get; set; }
        public int RegisterB { get; set; }
        public int InstructionIndex { get; private set; }

        public List<IInstruction> Instructions { get; }
        public IInstruction CurrentInstruction => Instructions[InstructionIndex];
        public bool IsValidInstruction => InstructionIndex >= 0 && InstructionIndex < Instructions.Count;

        public Computer(List<IInstruction> instructions)
        {
            Instructions = instructions;
        }

        internal void ApplyIndexOffset(int offset)
        {
            InstructionIndex += offset;
        }
    }
}
