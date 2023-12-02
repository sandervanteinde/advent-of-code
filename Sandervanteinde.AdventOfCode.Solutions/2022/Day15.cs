using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2022;

internal partial class Day15 : BaseSolution
{
    public Day15()
        : base("Beacon Exclusion Zone", year: 2022, day: 15)
    {
    }

    [GeneratedRegex(@"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)")]
    private partial Regex BeaconRegex();

    public override object DetermineStepOneResult(FileReader reader)
    {
        const int rowToCalc = 2000000;
        var sensors = ParseInput(reader);

        var pointsThatCannotBe = new HashSet<int>();

        foreach (var sensor in sensors)
        {
            var manhattanDistance = sensor.ManhattanDistance;

            var distanceFromLine = manhattanDistance - Math.Abs(sensor.Location.Y - rowToCalc);

            if (distanceFromLine < 0)
            {
                continue;
            }

            for (var i = -distanceFromLine; i <= distanceFromLine; i++)
            {
                pointsThatCannotBe.Add(sensor.Location.X + i);
            }
        }

        foreach (var sensor in sensors.SelectMany(sensor => new[] { sensor.Location, sensor.ClosestBeacon })
                     .Where(sensor => sensor.Y == rowToCalc))
        {
            pointsThatCannotBe.Remove(sensor.X);
            // pointsThatCannotBe.Remove(sensor.ClosestBeacon);
        }

        return pointsThatCannotBe.Count;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var sensors = ParseInput(reader);
        var point = new Point { X = 0, Y = 0 };
        bool inRangeOfASensor;

        do
        {
            inRangeOfASensor = false;

            foreach (var sensor in sensors)
            {
                var nextPoint = sensor.GetFirstPointOutOfRange(point);

                if (nextPoint is not null)
                {
                    inRangeOfASensor = true;
                    point = nextPoint.Value;
                    break;
                }
            }
        } while (inRangeOfASensor);

        return (point.X * (uint)4000000) + point.Y;
    }

    private IReadOnlyCollection<Sensor> ParseInput(FileReader reader)
    {
        var regex = BeaconRegex();
        var items = new List<Sensor>();

        foreach (var line in reader.ReadLineByLine())
        {
            var match = regex.Match(line);

            if (match is not
                {
                    Success: true, Groups: [_, { Value: string locationX }, { Value: string locationY }, { Value: string beaconX }, { Value: string beaconY }]
                })
            {
                throw new NotSupportedException($"Invalid line: {line}");
            }

            items.Add(
                new Sensor
                {
                    ClosestBeacon = new Point { X = int.Parse(beaconX), Y = int.Parse(beaconY) },
                    Location = new Point { X = int.Parse(locationX), Y = int.Parse(locationY) }
                }
            );
        }

        return items.AsReadOnly();
    }

    private class Sensor
    {
        private readonly Lazy<int> _manhattanDistance;

        public Sensor()
        {
            _manhattanDistance = new Lazy<int>(() => Point.ManhattanDistance(Location, ClosestBeacon));
        }

        public required Point Location { get; init; }
        public required Point ClosestBeacon { get; init; }
        public int ManhattanDistance => _manhattanDistance.Value;

        public Point? GetFirstPointOutOfRange(Point point)
        {
            var distance = Point.ManhattanDistance(point, Location);

            if (distance <= ManhattanDistance)
            {
                var newPoint = point with { X = point.X + (ManhattanDistance - distance) + 1 };

                if (newPoint.X > 4000000)
                {
                    return new Point { X = 0, Y = point.Y + 1 };
                }

                return newPoint;
            }

            return null;
        }
    }
}
