namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day13
{
    private class PointWithMutation
    {
        private readonly Point point;
        private Lazy<Point> lazyPoint;
        private Func<Point, Point> mutation;

        public PointWithMutation(Point point)
        {
            this.point = point;
            mutation = Noop;
            lazyPoint = new Lazy<Point>(point);
        }

        public Point Point => lazyPoint.Value;

        public void ApplyMutation(Func<Point, Point> mutation)
        {
            var oldMutation = this.mutation;
            this.mutation = point => mutation(oldMutation(point));
            lazyPoint = new Lazy<Point>(LazyInit);
        }

        public override string ToString()
        {
            return $"Original: {point}, Current: {Point}";
        }

        private static Point Noop(Point p)
        {
            return p;
        }

        private Point LazyInit()
        {
            return mutation(point);
        }
    }
}
