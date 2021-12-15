namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day15
{
    public class PointWithValue
    {
        public class CompareByValue : IComparer<PointWithValue>
        {
            public int Compare(PointWithValue? x, PointWithValue? y)
            {
                if (x!.Value == y!.Value)
                {
                    if (x.Point.X == y.Point.X)
                    {
                        return x.Point.Y.CompareTo(y.Point.Y);
                    }
                    return x.Point.X.CompareTo(y.Point.X);
                }
                return x!.Value.CompareTo(y!.Value);
            }
        }
        public Point Point { get; init; }
        public int Value { get; set; }
    }
}