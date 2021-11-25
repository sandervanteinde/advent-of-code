namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day07
{
    private class ConstantOperand : IOperand
    {
        private readonly int constantValue;

        public ConstantOperand(int constantValue)
        {
            this.constantValue = constantValue;
        }

        public int GetResult(IReadOnlyDictionary<string, int> values)
        {
            return constantValue;
        }

        public bool CanPerform(IReadOnlyDictionary<string, int> values)
        {
            return true;
        }
    }
}
