namespace Sandervanteinde.AdventOfCode2021.Solutions.Utils;
public record struct Point
{
    public int X { get; init; }
    public int Y { get; init; }

    public int GetId()
    {
        return X * 1000 + Y;
    }

    public int DistanceFromOrigin()
    {
        return Math.Abs(X) + Math.Abs(Y);
    }

    public static IEnumerable<Point> BetweenExclusiveStartInclusiveEnd(Point start, Point end)
    {
        var startX = start.X < end.X ? start.X : end.X;
        var endX = startX == start.X ? start.X : start.X;
        var startY = start.Y < end.Y ? start.Y : end.Y;
        var endY = startY == start.Y ? end.Y : start.Y;
        for (var x = startX; x <= endX; x++)
        {
            for (var y = startY; y <= endY; y++)
            {
                var point = new Point { X = x, Y = y };
                if (point == start)
                {
                    continue;
                }

                yield return new Point { X = x, Y = y };
            }
        }
    }
}
