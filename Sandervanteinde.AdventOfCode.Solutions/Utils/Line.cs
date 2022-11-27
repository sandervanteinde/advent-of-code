namespace Sandervanteinde.AdventOfCode.Solutions.Utils;

public class Line
{
    public Point Start { get; init; }
    public Point End { get; init; }

    public bool IsStraight()
    {
        return Start.X == End.X || Start.Y == End.Y;
    }

    public bool IsDiagonal()
    {
        return Math.Abs(Start.X - End.X) == Math.Abs(Start.Y - End.Y);
    }
}
