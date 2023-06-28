using Sandervanteinde.AdventOfCode.Solutions.Extensions;

namespace Sandervanteinde.AdventOfCode.Solutions._2022;
internal class Day08 : BaseSolution
{
    public Day08()
        : base("Treetop Tree House", 2022, 8)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var grid = reader.ReadAsIntegerGrid();
        var xSize = grid.GetLength(0);
        var ySize = grid.GetLength(1);
        var visiblePoints = new HashSet<Point>();
        var highestSoFar = 0;
        for(var x = 0; x < xSize; x++)
        {
            highestSoFar = grid[x, 0].Value;
            visiblePoints.Add(new Point { X = x, Y = 0 });
            for(var i = 1; i < ySize; i++)
            {
                if (grid[x, i].Value > highestSoFar)
                {
                    visiblePoints.Add(new Point { X = x, Y = i });
                    highestSoFar = grid[x, i].Value;
                }
            }

            visiblePoints.Add(new Point { X = x, Y = ySize - 1 });
            highestSoFar = grid[x, ySize - 1].Value;
            for(var i = ySize - 2; i >= 0; i--)
            {
                if (grid[x, i].Value > highestSoFar)
                {
                    visiblePoints.Add(new Point { X = x, Y = i });
                    highestSoFar = grid[x, i].Value;
                }
            }
        }
        for (var y = 0; y < ySize; y++)
        {
            visiblePoints.Add(new Point { X = 0, Y = y });
            highestSoFar = grid[0, y].Value;
            for (var i = 1; i < xSize; i++)
            {
                if (grid[i, y].Value > highestSoFar)
                {
                    visiblePoints.Add(new Point { X = i, Y = y });
                    highestSoFar = grid[i, y].Value;
                }
            }

            visiblePoints.Add(new Point { X = xSize - 1, Y = y });
            highestSoFar = grid[xSize - 1, y].Value;
            for (var i = xSize - 2; i >= 0; i--)
            {
                if (grid[i, y].Value > highestSoFar)
                {
                    visiblePoints.Add(new Point { X = i, Y = y });
                    highestSoFar = grid[i, y].Value;
                }
            }
        }

        return visiblePoints.Count;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var grid = reader.ReadAsIntegerGrid();
        var xLength = grid.GetLength(0);
        var yLength = grid.GetLength(1);
        var result = int.MinValue;
        foreach(var item in grid.EnumerateEntries())
        {
            var ownHeight = item.Value;
            // check left
            var leftViewLength = 0;
            for(var x = item.X - 1; x >= 0; x--)
            {
                leftViewLength++;
                if (grid[x, item.Y].Value >= ownHeight)
                {
                    break;
                }
            }
            if(leftViewLength == 0)
            {
                continue;
            }
            var rightViewLength = 0;
            for (var x = item.X + 1; x < xLength; x++)
            {
                rightViewLength++;
                if (grid[x, item.Y].Value >= ownHeight)
                {
                    break;
                }
            }
            if(rightViewLength == 0)
            {
                continue;
            }
            var topLength = 0;
            for(var y = item.Y - 1; y >= 0; y--)
            {
                topLength++;
                if (grid[item.X, y].Value >= ownHeight)
                {
                    break;
                }
            }
            if(topLength == 0)
            {
                continue;
            }
            var bottomLength = 0;

            for (var y = item.Y + 1; y < yLength; y++)
            {
                bottomLength++;
                if (grid[item.X, y].Value >= ownHeight)
                {
                    break;
                }
            }

            result = Math.Max(result, leftViewLength * rightViewLength * topLength * bottomLength);
        }
        return result;
    }
}
