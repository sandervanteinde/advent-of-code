namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day23
{
    private interface IInstruction
    {
        public bool ShouldAutoIncrement()
        {
            return true;
        }

        void PerformInstruction(Computer computer);
    }
}
