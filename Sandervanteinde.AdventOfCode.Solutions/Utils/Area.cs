namespace Sandervanteinde.AdventOfCode.Solutions.Utils;

internal class Area
{
    public int Left { get; set; }
    public int Top { get; set; }
    public int Right { get; set; }
    public int Bottom { get; set; }
    public Point TopLeft => new Point { X = Left, Y = Top };
    public Point TopRight => new Point { X = Right, Y = Top };
    public Point BottomRight => new Point { X = Right, Y = Bottom };
    public Point BottomLeft => new Point { X = Left, Y = Bottom };

    public void ExpandToFit(Point point)
    {
        ExpandToFit(point.X, point.Y);
    }

    public IEnumerable<Point> IteratePointsInArea()
    {
        for (var y = Top; y <= Bottom; y++)
        {
            for (var x = Left; x <= Right; x++)
            {
                yield return new Point { X = x, Y = y };
            }
        }
    }

    public void ExpandToFit(int x, int y)
    {
        Left = Math.Min(Left, x);
        Right = Math.Max(Right, x);
        Top = Math.Min(Top, y);
        Bottom = Math.Max(Bottom, y);
    }

    public bool IsInArea(Point p)
    {
        return p.X >= Left
            && p.X <= Right
            && p.Y >= Top
            && p.Y <= Bottom;
    }

    public static Area FromTopLeftAndBottomRight(Point topLeft, Point bottomRight)
    {
        return new Area
        {
            Top = topLeft.Y,
            Left = topLeft.X,
            Right = bottomRight.X,
            Bottom = bottomRight.Y
        };
    }
}
