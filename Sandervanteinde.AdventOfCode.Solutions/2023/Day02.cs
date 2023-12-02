using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2023;

internal partial class Day02 : BaseSolution
{
    public Day02()
        : base("Cube Conundrum", year: 2023, day: 2)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var regex = ColorRegex();
        var groupRegex = GroupRegex();
        var sum = 0;
        foreach (var line in reader.ReadLineByLine())
        {
            var matches = regex.Matches(line);

            foreach (Match match in matches)
            {
                var color = match.Groups[2].Value;
                var amount = int.Parse(match.Groups[1].Value);

                var isPossible = (color, amount) is ("green", <= 13) or ("red", <= 12) or ("blue", <= 14);

                if (!isPossible)
                {
                    goto notPossible;
                }
            }
            
            var groupMatch = groupRegex.Match(line);
            sum += int.Parse(groupMatch.Groups[1].Value);
            
            notPossible: ;
        }

        return sum;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var regex = ColorRegex();
        var sum = 0L;
        foreach (var line in reader.ReadLineByLine())
        {
            var matches = regex.Matches(line);

            var reds = long.MinValue;
            var blues = long.MinValue;
            var greens = long.MinValue;
            
            foreach (Match match in matches)
            {
                var color = match.Groups[2].Value;
                var amount = long.Parse(match.Groups[1].Value);

                (reds, blues, greens) = color switch
                {
                    "green" => (reds, blues, Math.Max(greens, amount)),
                    "red" => (Math.Max(reds, amount), blues, greens),
                    "blue" => (reds, Math.Max(blues, amount), greens),
                    _ => throw new NotSupportedException($"Color {color} should not be possible")
                };
            }

            var valueOfGame = reds * blues * greens;
            sum += valueOfGame;
        }

        return sum;
    }

    [GeneratedRegex(@"(\d+) (blue|red|green)")]
    public static partial Regex ColorRegex();

    [GeneratedRegex(@"Game (\d+):")]
    public static partial Regex GroupRegex();
}

