namespace Sandervanteinde.AdventOfCode2021.Solutions.Utils;
public struct Point
{
    public int X { get; init; }
    public int Y { get; init; }

    public int GetId()
    {
        return X * 1000 + Y;
    }
}
