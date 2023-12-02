namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day07
{
    private struct MemoryAddressOrConstant
    {
        public int Constant { get; }
        public string? MemoryAddress { get; }
        public bool IsMemoryAddress => MemoryAddress is not null;

        private MemoryAddressOrConstant(int constant)
        {
            Constant = constant;
            MemoryAddress = null;
        }

        private MemoryAddressOrConstant(string memoryAddress)
        {
            Constant = -1;
            MemoryAddress = memoryAddress;
        }

        public int GetValue(IReadOnlyDictionary<string, int> memory)
        {
            if (IsMemoryAddress)
            {
                return memory[MemoryAddress!];
            }

            return Constant;
        }

        public static implicit operator MemoryAddressOrConstant(int constant)
        {
            return new MemoryAddressOrConstant(constant);
        }

        public static implicit operator MemoryAddressOrConstant(string memoryAddress)
        {
            return new MemoryAddressOrConstant(memoryAddress);
        }
    }
}
