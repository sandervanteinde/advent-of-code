namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day07
{
    private class CopyMemoryAddressOperand : IOperand
    {
        private readonly string memoryAddress;

        public CopyMemoryAddressOperand(string memoryAddress)
        {
            this.memoryAddress = memoryAddress;
        }

        public bool CanPerform(IReadOnlyDictionary<string, int> values)
        {
            return values.ContainsKey(memoryAddress);
        }

        public int GetResult(IReadOnlyDictionary<string, int> values)
        {
            return values[memoryAddress];
        }
    }
}
