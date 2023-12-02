namespace Sandervanteinde.AdventOfCode.Solutions.Utils;

public static class CharReadOnlySpanExtensions
{
    public static SplitResult SplitAtFirstOccurenceOf(this ReadOnlySpan<char> input, char c)
    {
        var comma = input.IndexOf(c);
        var leftPart = input[..comma];
        var rightPart = input[(comma + 1)..];
        return new SplitResult { Left = leftPart, Right = rightPart };
    }
}

public ref struct SplitResult
{
    public required ReadOnlySpan<char> Left { get; init; }
    public required ReadOnlySpan<char> Right { get; init; }

    public void Deconstruct(out ReadOnlySpan<char> left, out ReadOnlySpan<char> right)
    {
        left = Left;
        right = Right;
    }
}
