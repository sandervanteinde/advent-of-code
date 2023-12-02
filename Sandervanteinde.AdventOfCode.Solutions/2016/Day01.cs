namespace Sandervanteinde.AdventOfCode.Solutions._2016;

internal class Day01 : BaseSolution
{
    public Day01()
        : base("No Time for a Taxicab", year: 2016, day: 1)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var directions = reader.Input
            .Split(", ")
            .Select(value => new { Direction = value[index: 0], Amount = int.Parse(value[1..]) })
            .ToArray();

        var availableDirections = new List<Func<Point, int, Point>> { North, East, South, West };
        var currentDirection = availableDirections[index: 0];
        var location = new Point();

        foreach (var direction in directions)
        {
            currentDirection = availableDirections[(availableDirections.IndexOf(currentDirection) + (direction.Direction == 'R'
                ? 1
                : -1) + 4) % 4];
            location = currentDirection(location, direction.Amount);
        }

        return location.DistanceFromOrigin();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var directions = reader.Input
            .Split(", ")
            .Select(value => new { Direction = value[index: 0], Amount = int.Parse(value[1..]) })
            .ToArray();

        var availableDirections = new List<Func<Point, int, Point>> { North, East, South, West };
        var currentDirection = availableDirections[index: 0];
        var location = new Point();
        var visited = new HashSet<Point>(new[] { location });

        foreach (var direction in directions)
        {
            currentDirection = availableDirections[(availableDirections.IndexOf(currentDirection) + (direction.Direction == 'R'
                ? 1
                : -1) + 4) % 4];

            for (var i = 0; i < direction.Amount; i++)
            {
                location = currentDirection(location, arg2: 1);

                if (!visited.Add(location))
                {
                    return location.DistanceFromOrigin();
                }
            }
        }

        throw new InvalidOperationException("Did not find result");
    }

    private Point North(Point point, int amount)
    {
        return point with { Y = point.Y + amount };
    }

    private Point East(Point point, int amount)
    {
        return point with { X = point.X + amount };
    }

    private Point West(Point point, int amount)
    {
        return point with { X = point.X - amount };
    }

    private Point South(Point point, int amount)
    {
        return point with { Y = point.Y - amount };
    }
}
