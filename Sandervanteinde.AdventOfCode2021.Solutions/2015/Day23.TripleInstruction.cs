namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day23
{
    private class TripleInstruction : IInstruction
    {
        public void PerformInstruction(Computer computer)
        {
            computer.RegisterA *= 3;
        }
    }
}
