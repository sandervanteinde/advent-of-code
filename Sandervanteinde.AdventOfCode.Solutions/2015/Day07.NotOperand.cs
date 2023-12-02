namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day07
{
    private class NotOperand : IOperand
    {
        private readonly MemoryAddressOrConstant left;

        public NotOperand(MemoryAddressOrConstant left)
        {
            this.left = left;
        }

        public bool CanPerform(IReadOnlyDictionary<string, int> values)
        {
            return !left.IsMemoryAddress || values.ContainsKey(left.MemoryAddress!);
        }

        public int GetResult(IReadOnlyDictionary<string, int> values)
        {
            return ~left.GetValue(values);
        }
    }
}
