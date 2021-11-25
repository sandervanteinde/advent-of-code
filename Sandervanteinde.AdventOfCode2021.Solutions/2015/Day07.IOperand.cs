namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day07
{
    private interface IOperand
    {
        int GetResult(IReadOnlyDictionary<string, int> values);
        bool CanPerform(IReadOnlyDictionary<string, int> values);
    }
}
