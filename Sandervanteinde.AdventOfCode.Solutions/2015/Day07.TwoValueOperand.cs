namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day07
{
    private class TwoValueOperand : IOperand
    {
        private readonly MemoryAddressOrConstant left;
        private readonly MemoryAddressOrConstant right;
        private readonly Func<int, int, int> operand;

        public TwoValueOperand(
            MemoryAddressOrConstant left,
            MemoryAddressOrConstant right,
            Func<int, int, int> operand
        )
        {
            this.left = left;
            this.right = right;
            this.operand = operand;
        }

        public bool CanPerform(IReadOnlyDictionary<string, int> values)
        {
            return (!left.IsMemoryAddress || values.ContainsKey(left.MemoryAddress!))
                && (!right.IsMemoryAddress || values.ContainsKey(right.MemoryAddress!));
        }

        public int GetResult(IReadOnlyDictionary<string, int> values)
        {
            return operand(left.GetValue(values), right.GetValue(values));
        }
    }
}
