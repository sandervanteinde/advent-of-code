using System.Text;
using Sandervanteinde.AdventOfCode.Solutions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode.Solutions._2016;

internal class Day06 : BaseSolution
{
    public Day06()
        : base(@"Signals and Noise", 2016, 6)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var inputByStringIndex = reader.ReadLineByLine()
            .SelectMany(line => line.Select((x, index) => new { Char = x, Index = index }))
            .GroupBy(x => x.Index)
            .OrderBy(x => x.Key);

        var sb = new StringBuilder();
        foreach (var input in inputByStringIndex)
        {
            var grouped = input.GroupBy(x => x.Char)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .First();
            sb.Append(grouped);
        }

        return sb.ToString();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var inputByStringIndex = reader.ReadLineByLine()
            .SelectMany(line => line.Select((x, index) => new { Char = x, Index = index }))
            .GroupBy(x => x.Index)
            .OrderBy(x => x.Key);

        var sb = new StringBuilder();
        foreach (var input in inputByStringIndex)
        {
            var grouped = input.GroupBy(x => x.Char)
                .OrderBy(x => x.Count())
                .Select(x => x.Key)
                .First();
            sb.Append(grouped);
        }

        return sb.ToString();
    }
}