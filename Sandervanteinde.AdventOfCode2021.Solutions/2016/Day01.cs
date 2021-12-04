namespace Sandervanteinde.AdventOfCode2021.Solutions._2016;

internal class Day01 : BaseSolution
{
    public Day01()
        : base("No Time for a Taxicab", 2016, 1)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var directions = reader.Input
            .Split(", ")
            .Select(value => new
            {
                Direction = value[0],
                Amount = int.Parse(value[1..])
            })
            .ToArray();

        var availableDirections = new List<Func<Point, int, Point>> { North, East, South, West };
        var currentDirection = availableDirections[0];
        var location = new Point();

        foreach (var direction in directions)
        {
            currentDirection = availableDirections[((availableDirections.IndexOf(currentDirection) + (direction.Direction == 'R' ? 1 : -1) + 4) % 4)];
            location = currentDirection(location, direction.Amount);
        }

        return location.DistanceFromOrigin();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        throw new NotImplementedException("Not yet implemented");
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
