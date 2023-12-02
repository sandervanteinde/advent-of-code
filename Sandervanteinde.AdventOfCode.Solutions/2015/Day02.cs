using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal class Day02 : BaseSolution
{
    public Day02()
        : base("I Was Told There Would Be No Math", year: 2015, day: 2)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var regex = new Regex("(\\d+)x(\\d+)x(\\d+)", RegexOptions.Compiled);
        uint total = 0;

        foreach (var (length, width, height) in GetPresentDimensions(reader))
        {
            var wrappingPaperRequired = (length * width * 2) + (length * height * 2) + (width * height * 2);
            var slackSizes = new[] { length, width, height }
                .OrderBy(i => i)
                .Take(count: 2)
                .ToArray();
            var slack = slackSizes[0] * slackSizes[1];

            total += wrappingPaperRequired + slack;
        }

        return total;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        uint total = 0;

        foreach (var (length, width, height) in GetPresentDimensions(reader))
        {
            var smallestSides = new[] { length, width, height }
                .OrderBy(i => i)
                .Take(count: 2)
                .ToArray();
            var ribbonForWrap = (smallestSides[0] * 2) + (smallestSides[1] * 2);
            var ribbonForBow = length * width * height;
            total += ribbonForBow + ribbonForWrap;
        }

        return total;
    }

    private static IEnumerable<(uint length, uint width, uint height)> GetPresentDimensions(FileReader reader)
    {
        var regex = new Regex("(\\d+)x(\\d+)x(\\d+)", RegexOptions.Compiled);
        return reader.ReadLineByLine()
            .Select(
                line =>
                {
                    var match = regex.Match(line);

                    if (!match.Success)
                    {
                        throw new NotSupportedException($"The line {line} did not meet the input requirements");
                    }

                    var length = uint.Parse(match.Groups[groupnum: 1].Value);
                    var width = uint.Parse(match.Groups[groupnum: 2].Value);
                    var height = uint.Parse(match.Groups[groupnum: 3].Value);
                    return (length, width, height);
                }
            );
    }
}
