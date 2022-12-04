using System.Diagnostics;
using Sandervanteinde.AdventOfCode.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode.Solutions._2022;

internal class Day04 : BaseSolution
{
    public Day04()
        : base("Camp Cleanup", 2022, 4)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var counter = 0;
        foreach(var line in reader.ReadAsSpanLineByLine())
        {
            var (left, right) = line.SplitAtFirstOccurenceOf(',');
            var (leftStart, leftEnd) = left.SplitAtFirstOccurenceOf('-');
            var (rightStart, rightEnd) = right.SplitAtFirstOccurenceOf('-');
            var leftRange = new Range(int.Parse(leftStart), int.Parse(leftEnd));
            var rightRange = new Range(int.Parse(rightStart), int.Parse(rightEnd));
            if(leftRange.FullyContains(rightRange) || rightRange.FullyContains(leftRange))
            {
                counter++;
            }
        }

        return counter;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var counter = 0;
        foreach (var line in reader.ReadAsSpanLineByLine())
        {
            var (left, right) = line.SplitAtFirstOccurenceOf(',');
            var (leftStart, leftEnd) = left.SplitAtFirstOccurenceOf('-');
            var (rightStart, rightEnd) = right.SplitAtFirstOccurenceOf('-');
            var leftRange = new Range(int.Parse(leftStart), int.Parse(leftEnd));
            var rightRange = new Range(int.Parse(rightStart), int.Parse(rightEnd));
            if (leftRange.PartiallyOverlaps(rightRange))
            {
                counter++;
                Console.WriteLine($"{leftRange}\t{rightRange}");
            }
        }

        return counter;
    }

    public static SplitResult SplitAtFirstOccurenceOf(ReadOnlySpan<char> input, char c)
    {
        var comma = input.IndexOf(c);
        var leftPart = input[..comma];
        var rightPart = input[(comma + 1)..];
        return new SplitResult { Left = leftPart, Right = rightPart };
    }
}

file record Range(int From, int To)
{
    public bool FullyContains(Range other)
    {
        return From >= other.From && To <= other.To;
    }

    public bool PartiallyOverlaps(Range other)
    {
        if(other.From > To || other.To < From)
        {
            return false;
        }

        return true;

    }
}
