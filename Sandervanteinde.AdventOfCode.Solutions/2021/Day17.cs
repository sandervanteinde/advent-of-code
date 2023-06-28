using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal class Day17 : BaseSolution
{
    public Day17()
        : base("Trick Shot", 2021, 17)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var targetArea = ParseInput(reader);
        var highestY = int.MinValue;
        for (var y = 0; y < 100; y++)
        {
            for (var x = 0; x < y; x++)
            {
                var velocity = new Point { X = x, Y = y };
                var locations = ProbeLocations(velocity)
                    .TakeWhile(p => p.Y >= targetArea.TopLeft.Y);
                var maxY = int.MinValue;
                foreach (var location in locations)
                {
                    maxY = Math.Max(location.Y, maxY);
                    if (targetArea.IsInArea(location))
                    {
                        highestY = Math.Max(maxY, highestY);
                        break;
                    }
                }
            }
        }
        return highestY;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        const int RANDOM_AMOUNT_OF_ATTEMPTS = 1000;
        var targetArea = ParseInput(reader);
        var amount = 0;
        for (var y = -RANDOM_AMOUNT_OF_ATTEMPTS; y < RANDOM_AMOUNT_OF_ATTEMPTS; y++)
        {
            for (var x = 0; x < RANDOM_AMOUNT_OF_ATTEMPTS; x++)
            {
                var velocity = new Point { X = x, Y = y };
                var locations = ProbeLocations(velocity)
                    .TakeWhile(p => p.Y >= targetArea.TopLeft.Y && p.X <= targetArea.BottomRight.X);
                var maxY = int.MinValue;
                foreach (var location in locations)
                {
                    maxY = Math.Max(location.Y, maxY);
                    if (targetArea.IsInArea(location))
                    {
                        amount++;
                        break;
                    }
                }
            }
        }
        return amount;
    }

    private static IEnumerable<Point> ProbeLocations(Point velocity)
    {
        var current = new Point { X = 0, Y = 0 };
        while (true)
        {
            current = new Point
            {
                X = current.X + velocity.X,
                Y = current.Y + velocity.Y,
            };

            yield return current;

            velocity = new Point
            {
                X = velocity.X == 0 ? 0 : velocity.X - 1,
                Y = velocity.Y - 1
            };
        }
    }

    private static Area ParseInput(FileReader reader)
    {
        var regex = new Regex(@"target area: x=(-?\d+)\.\.(-?\d+), y=(-?\d+)\.\.(-?\d+)");
        var match = regex.Match(reader.Input);
        if (!match.Success)
        {
            throw new InvalidOperationException("Invalid input");
        }

        var startPoint = new Point
        {
            X = int.Parse(match.Groups[1].Value),
            Y = int.Parse(match.Groups[3].Value)
        };

        var endPoint = new Point
        {
            X = int.Parse(match.Groups[2].Value),
            Y = int.Parse(match.Groups[4].Value)
        };

        return Area.FromTopLeftAndBottomRight(startPoint, endPoint);
    }
}
