using OneOf;

namespace Sandervanteinde.AdventOfCode.Solutions._2022;
internal class Day14 : BaseSolution
{
    public Day14()
        : base("Regolith Reservoir", 2022, 14)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var rocks = new HashSet<Point>();
        foreach (var line in GetPathsFromReader(reader))
        {
            for (var i = 1; i < line.Length; i++)
            {
                var from = line[i - 1];
                var to = line[i];
                foreach (var p in Point.BetweenInclusive(from, to))
                {
                    rocks.Add(p);
                }
            }
        }

        var lowestY = rocks.Max(r => r.Y);
        var rockCount = rocks.Count;

        while (true)
        {
            var sandPoint = DetermineNextSandRestPoint(rocks, lowestY);
            if (sandPoint.IsT1)
            {
                break;
            }
            rocks.Add(sandPoint.AsT0);
        }

        return rocks.Count - rockCount;
    }
    public override object DetermineStepTwoResult(FileReader reader)
    {
        var rocks = new HashSet<Point>();
        foreach (var line in GetPathsFromReader(reader))
        {
            for (var i = 1; i < line.Length; i++)
            {
                var from = line[i - 1];
                var to = line[i];
                foreach (var p in Point.BetweenInclusive(from, to))
                {
                    rocks.Add(p);
                }
            }
        }

        var lowestY = rocks.Max(r => r.Y);
        var rockCount = rocks.Count;

        while (true)
        {
            var sandPoint = DetermineNextSandRestPoint(rocks, lowestY, hasFloor: true);
            if (sandPoint.IsT2)
            {
                rocks.Add(new Point { X = 500, Y = 0 });
                break;
            }
            rocks.Add(sandPoint.AsT0);
        }

        return rocks.Count - rockCount;
    }

    private record struct VoidReached();
    private record struct CantFall();
    private record struct IsAtFeed();

    private static OneOf<Point, VoidReached, IsAtFeed> DetermineNextSandRestPoint(HashSet<Point> blockades, int lowestY, bool hasFloor = false)
    {
        var sandPoint = new Point { X = 500, Y = 0 };
        var next = TryFall(sandPoint);
        if (next.IsT1)
        {
            return next.AsT1;
        }
        if (next.IsT2)
        {
            return new IsAtFeed();
        }
        while (next.IsT0)
        {
            sandPoint = next.AsT0;
            next = TryFall(sandPoint);
        }
        return next.Match<OneOf<Point, VoidReached, IsAtFeed>>(
            p => throw new NotSupportedException("When a point is returned we should not be in this state."),
            voidReached => voidReached,
            cantFall => sandPoint
        );

        OneOf<Point, VoidReached, CantFall> TryFall(Point previous)
        {
            var nextDown = previous with { Y = previous.Y + 1 };
            if (nextDown.Y > lowestY)
            {
                if (!hasFloor)
                {
                    return new VoidReached();
                }
                if (nextDown.Y == lowestY + 2)
                {
                    return new CantFall();
                }
            }
            if (!blockades.Contains(nextDown))
            {
                return nextDown;
            }
            nextDown = previous with { Y = previous.Y + 1, X = previous.X - 1 };
            if (!blockades.Contains(nextDown))
            {
                return nextDown;
            }

            nextDown = nextDown with { X = previous.X + 1 };
            if (!blockades.Contains(nextDown))
            {
                return nextDown;
            }

            return new CantFall();
        }
    }

    private static IEnumerable<Point[]> GetPathsFromReader(FileReader reader)
    {
        foreach (var line in reader.ReadLineByLine())
        {
            var points = line.Split(" -> ");
            var result = new Point[points.Length];
            for (var i = 0; i < points.Length; i++)
            {
                var point = points[i];
                var pointCoords = point.Split(',');
                var actualPoint = new Point { X = int.Parse(pointCoords[0]), Y = int.Parse(pointCoords[1]) };
                result[i] = actualPoint;
            }
            yield return result;
        }
    }
}
