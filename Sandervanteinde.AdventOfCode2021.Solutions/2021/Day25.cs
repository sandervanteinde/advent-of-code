namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal class Day25 : BaseSolution
{
    public enum Facing
    {
        None, Right, Down
    }
    public Day25()
        : base(@"Sea Cucumber", 2021, 25)
    {

    }

    private class PointComparer : IComparer<Point>
    {
        public static PointComparer Instance { get; } = new();
        public int Compare(Point left, Point right)
        {
            return left == right ? 0
                : left.Y == right.Y
                    ? left.X - right.X
                    : left.Y - right.Y;
        }
    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var grid = reader.ReadAsGrid(c => c switch
        {
            '.' => Facing.None,
            '>' => Facing.Right,
            'v' => Facing.Down,
            _ => throw new ArgumentException("Invalid char " + c)
        });

        var lookupDictionary = new SortedDictionary<Point, Facing>(PointComparer.Instance);

        var maxX = grid.GetLength(0);
        var maxY = grid.GetLength(1);
        for (var y = 0; y < maxY; y++)
        {
            for (var x = 0; x < maxX; x++)
            {
                var value = grid[x, y].Value;
                if (value != Facing.None)
                {
                    lookupDictionary.Add(new Point { X = x, Y = y }, value);
                }
            }
        }

        bool hasMoved;
        var iterations = 0;
        do
        {
            hasMoved = false;
            var newDictionary = new SortedDictionary<Point, Facing>(PointComparer.Instance);
            var perHerd = lookupDictionary.ToLookup(x => x.Value, x => x.Key);
            foreach (var eastFacingHerd in perHerd[Facing.Right])
            {
                var newTarget = DetermineTargetForEastFacing(eastFacingHerd);
                newDictionary.Add(newTarget, Facing.Right);
            }

            foreach (var southFacingHerd in perHerd[Facing.Down])
            {
                var newTarget = DetermineTargetForSouthFacing(southFacingHerd, newDictionary);
                newDictionary.Add(newTarget, Facing.Down);
            }
            iterations++;
            lookupDictionary = newDictionary;
        }
        while (hasMoved);

        return iterations;

        Point DetermineTargetForEastFacing(Point currentPosition)
        {
            var newPosition = currentPosition with { X = (currentPosition.X + 1) % maxX };
            if (lookupDictionary.ContainsKey(newPosition))
            {
                return currentPosition;
            }

            hasMoved = true;
            return newPosition;
        }

        Point DetermineTargetForSouthFacing(Point currentPosition, SortedDictionary<Point, Facing> targets)
        {
            var newPosition = currentPosition with { Y = (currentPosition.Y + 1) % maxY };
            if (targets.ContainsKey(newPosition))
            {
                return currentPosition;
            }
            if (lookupDictionary.TryGetValue(newPosition, out var existingCucumber) && existingCucumber == Facing.Down)
            {
                return currentPosition;
            }
            hasMoved = true;
            return newPosition;
        }
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        return "freebee :)";
    }
}
