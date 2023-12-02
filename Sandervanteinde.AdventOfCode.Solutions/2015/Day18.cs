namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal class Day18 : BaseSolution
{
    public Day18()
        : base("Like a GIF For Your Yard", year: 2015, day: 18)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var grid = ParseToGrid(reader);

        for (var i = 0; i < 100; i++)
        {
            var newGrid = new bool[grid.GetLength(dimension: 0), grid.GetLength(dimension: 1)];

            foreach (var window in GridWindow<bool>.EnumerateGrid(grid))
            {
                var total = 0;

                foreach (var surrounding in window.Surroundings())
                {
                    if (surrounding.value)
                    {
                        total++;
                    }
                }

                if (window.Value)
                {
                    newGrid[window.X, window.Y] = total is 2 or 3;
                }
                else
                {
                    newGrid[window.X, window.Y] = total is 3;
                }
            }

            grid = newGrid;
        }

        var totalOn = 0;

        for (var y = 0; y < grid.GetLength(dimension: 1); y++)
        {
            for (var x = 0; x < grid.GetLength(dimension: 0); x++)
            {
                if (grid[x, y])
                {
                    totalOn++;
                }
            }
        }

        return totalOn;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var grid = ParseToGrid(reader);
        grid[0, 0] = grid[0, 99] = grid[99, 0] = grid[99, 99] = true;

        for (var i = 0; i < 100; i++)
        {
            var newGrid = new bool[grid.GetLength(dimension: 0), grid.GetLength(dimension: 1)];

            foreach (var window in GridWindow<bool>.EnumerateGrid(grid))
            {
                var total = 0;

                foreach (var surrounding in window.Surroundings())
                {
                    if (surrounding.value)
                    {
                        total++;
                    }
                }

                if (window.IsCorner)
                {
                    newGrid[window.X, window.Y] = true;
                    continue;
                }

                if (window.Value)
                {
                    newGrid[window.X, window.Y] = total is 2 or 3;
                }
                else
                {
                    newGrid[window.X, window.Y] = total is 3;
                }
            }

            grid = newGrid;
        }

        var totalOn = 0;

        for (var y = 0; y < grid.GetLength(dimension: 1); y++)
        {
            for (var x = 0; x < grid.GetLength(dimension: 0); x++)
            {
                if (grid[x, y])
                {
                    totalOn++;
                }
            }
        }

        return totalOn;
    }

    private static bool[,] ParseToGrid(FileReader reader)
    {
        var index = 0;
        var lines = reader.ReadLineByLine()
            .ToArray();
        var lineLength = lines.First()
            .Length;

        var grid = new bool[lines.Length, lineLength];

        foreach (var line in reader.ReadLineByLine())
        {
            var cIndex = 0;

            foreach (var c in line)
            {
                grid[index, cIndex] = c == '#';
                cIndex++;
            }

            index++;
        }

        return grid;
    }
}
