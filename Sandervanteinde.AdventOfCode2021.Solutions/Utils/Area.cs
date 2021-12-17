namespace Sandervanteinde.AdventOfCode2021.Solutions.Utils;

internal class Area
{
    public Point TopLeft { get; init; }
    public Point TopRight => new Point { X = BottomRight.X, Y = TopLeft.Y };
    public Point BottomRight { get; init; }
    public Point BottomLeft => new Point { X = TopLeft.X, Y = BottomRight.Y };

    public bool IsInArea(Point p)
    {
        return p.X >= TopLeft.X
            && p.X <= BottomRight.X
            && p.Y >= TopLeft.Y
            && p.Y <= BottomRight.Y;
    }

    public static Area FromTopLeftAndBottomRight(Point topLeft, Point bottomRight)
    {
        return new Area
        {
            TopLeft = topLeft,
            BottomRight = bottomRight
        };
    }
}
