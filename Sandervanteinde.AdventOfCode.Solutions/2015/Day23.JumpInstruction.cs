namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day23
{
    private class JumpInstruction : IInstruction
    {
        private readonly int offset;
        private readonly Predicate<Computer> shouldJump;

        public JumpInstruction(int offset, Predicate<Computer> shouldJump)
        {
            this.offset = offset;
            this.shouldJump = shouldJump;
        }

        public void PerformInstruction(Computer computer)
        {
            var indexOfCurrent = computer.Instructions.IndexOf(this);
            if (shouldJump(computer))
            {
                computer.ApplyIndexOffset(offset);
            }
            else
            {
                computer.ApplyIndexOffset(1);
            }
        }

        public bool ShouldAutoIncrement()
        {
            return false;
        }
    }
}
