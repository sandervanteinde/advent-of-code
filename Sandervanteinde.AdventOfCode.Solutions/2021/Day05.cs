using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal class Day05 : BaseSolution
{
    public Day05()
        : base("Hydrothermal Venture", 2021, 5)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        return ParseLines(reader)
            .Where(line => line.IsStraight())
            .SelectMany(line => Point.BetweenInclusive(line.Start, line.End))
            .GroupBy(point => point)
            .Where(grouping => grouping.Count() >= 2)
            .Count();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        return ParseLines(reader)
            .SelectMany(line => line.IsDiagonal() ? Point.DiagonalInclusive(line) : Point.BetweenInclusive(line.Start, line.End))
            .GroupBy(point => point)
            .Where(grouping => grouping.Count() >= 2)
            .Count();
    }
    private IEnumerable<Line> ParseLines(FileReader reader)
    {
        var regex = new Regex(@"(\d+),(\d+) -> (\d+),(\d+)");
        foreach (var match in reader.MatchLineByLine(regex))
        {
            var line = new Line
            {
                Start = new()
                {
                    X = int.Parse(match.Groups[1].Value),
                    Y = int.Parse(match.Groups[2].Value)
                },
                End = new()
                {
                    X = int.Parse(match.Groups[3].Value),
                    Y = int.Parse(match.Groups[4].Value)
                }
            };
            yield return line;
        }
    }
}
