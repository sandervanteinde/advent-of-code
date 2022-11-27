using Sandervanteinde.AdventOfCode.Solutions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day15 : BaseSolution
{
    public Day15()
        : base("Chiton", 2021, 15)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var grid = reader.ReadAsIntegerGrid();
        var xLength = grid.GetLength(0);
        var yLength = grid.GetLength(1);
        var distanceGrid = new int[grid.GetLength(0), grid.GetLength(1)];
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                distanceGrid[x, y] = int.MaxValue;
            }
        }

        var visited = new HashSet<Point>();
        distanceGrid[0, 0] = 0;
        var current = new Point { X = 0, Y = 0 };
        var sortedList = new SortedSet<PointWithValue>(new PointWithValue.CompareByValue());
        while (current.X != xLength - 1 || current.Y != yLength - 1)
        {
            foreach (var adjacent in grid[current.X, current.Y].AdjacentNonDiagonals())
            {
                var point = new Point { X = adjacent.x, Y = adjacent.y };
                if (visited.Contains(point))
                {
                    continue;
                }

                var newDistance = Math.Min(distanceGrid[point.X, point.Y], distanceGrid[current.X, current.Y] + adjacent.value);
                distanceGrid[point.X, point.Y] = Math.Min(distanceGrid[point.X, point.Y], distanceGrid[current.X, current.Y] + adjacent.value);
                sortedList.Add(new PointWithValue { Point = point, Value = newDistance });
            }
            visited.Add(current);

            var next = sortedList.First();
            sortedList.Remove(next);
            current = next.Point;
        }
        return distanceGrid[xLength - 1, yLength - 1];
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var grid = reader.ReadAsIntegerGrid();
        grid = MakeFiveTimesLarger(grid);
        var xLength = grid.GetLength(0);
        var yLength = grid.GetLength(1);
        var distanceGrid = new int[grid.GetLength(0), grid.GetLength(1)];
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                distanceGrid[x, y] = int.MaxValue;
            }
        }

        var visited = new HashSet<Point>();
        distanceGrid[0, 0] = 0;
        var current = new Point { X = 0, Y = 0 };
        var sortedList = new SortedSet<PointWithValue>(new PointWithValue.CompareByValue());
        while (current.X != xLength - 1 || current.Y != yLength - 1)
        {
            foreach (var adjacent in grid[current.X, current.Y].AdjacentNonDiagonals())
            {
                var point = new Point { X = adjacent.x, Y = adjacent.y };
                if (visited.Contains(point))
                {
                    continue;
                }

                var newDistance = Math.Min(distanceGrid[point.X, point.Y], distanceGrid[current.X, current.Y] + adjacent.value);
                distanceGrid[point.X, point.Y] = Math.Min(distanceGrid[point.X, point.Y], distanceGrid[current.X, current.Y] + adjacent.value);
                sortedList.Add(new PointWithValue { Point = point, Value = newDistance });
            }
            visited.Add(current);

            var next = sortedList.First();
            sortedList.Remove(next);
            current = next.Point;
        }
        return distanceGrid[xLength - 1, yLength - 1];
    }

    private GridWindow<int>[,] MakeFiveTimesLarger(GridWindow<int>[,] grid)
    {
        var xLength = grid.GetLength(0);
        var yLength = grid.GetLength(1);
        var newGrid = new int[xLength * 5, yLength * 5];
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                var gridValue = grid[x, y].Value;
                for (var dy = 0; dy < 5; dy++)
                {
                    for (var dx = 0; dx < 5; dx++)
                    {
                        newGrid[dx * xLength + x, dy * yLength + y] = CutoffMoreThan9(gridValue + dx + dy);
                    }
                }
            }
        }
        return newGrid.ToGridwindows();

        static int CutoffMoreThan9(int value)
        {
            if (value <= 9)
            {
                return value;
            }
            return value - 9;
        }
    }
}