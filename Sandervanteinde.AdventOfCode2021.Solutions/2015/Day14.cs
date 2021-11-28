using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day14 : BaseSolution
{
    private const int EVALUATION_TIME = 2503;
    public Day14()
        : base("Reindeer Olympics", 2015, 14)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        return ParseReindeerStats(reader)
            .Max(reindeerStats => reindeerStats.DistanceTravelledAfter(EVALUATION_TIME));
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var reindeers = ParseReindeerStats(reader).ToArray();
        var points = reindeers.ToDictionary(
            reindeer => reindeer,
            reindeer => 0 // points
        );

        for (var i = 1; i <= EVALUATION_TIME; i++)
        {
            var distanceFlown = reindeers.ToDictionary(
                reindeer => reindeer,
                reindeer => reindeer.DistanceTravelledAfter(i)
            );

            int? firstDistance = null;
            foreach (var kvp in distanceFlown.OrderByDescending(kvp => kvp.Value))
            {
                if (firstDistance is null || kvp.Value == firstDistance)
                {
                    points[kvp.Key] += 1;
                    firstDistance = kvp.Value;
                }
                else
                {
                    break;
                }
            }
        }

        return points.Max(point => point.Value);
    }

    private static IEnumerable<ReindeerStats> ParseReindeerStats(FileReader reader)
    {
        var regex = new Regex(@"([A-Za-z]+) can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds.");
        foreach (var match in reader.MatchLineByLine(regex))
        {
            var reindeerName = match.Groups[1].Value;
            var speedInKms = int.Parse(match.Groups[2].Value);
            var flyDuration = int.Parse(match.Groups[3].Value);
            var restTimeInSeconds = int.Parse(match.Groups[4].Value);
            yield return new ReindeerStats
            {
                ReindeerName = reindeerName,
                SpeedInKms = speedInKms,
                FlyDurationInSeconds = flyDuration,
                RestTimeInSeconds = restTimeInSeconds,
                CycleTime = flyDuration + restTimeInSeconds
            };
        }
    }
}
