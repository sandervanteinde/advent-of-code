namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day09 : BaseSolution
{
    public Day09()
        : base("Smoke Basin", 2021, 9)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var grid = reader.ReadAsIntegerGrid();
        var sum = 0;
        var xLength = grid.GetLength(0);
        var yLength = grid.GetLength(1);
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                var window = grid[x, y];
                var isLowestPoint = true;
                foreach (var adjacent in window.AdjacentNonDiagonals())
                {
                    if (adjacent.value <= window.Value)
                    {
                        isLowestPoint = false;
                        break;
                    }
                }
                if (isLowestPoint)
                {
                    sum += 1 + window.Value;
                }
            }
        }
        return sum;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var grid = reader.ReadAsIntegerGrid();
        var xLength = grid.GetLength(0);
        var yLength = grid.GetLength(1);
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                DetermineBasin(grid, grid[x, y]);
            }
        }

        var biggest = GridUtils.EnumerateGrid(grid)
            .Select(grid => grid.Tag)
            .OfType<Basin>()
            .Distinct()
            .Select(x => x.Items.Count)
            .OrderByDescending(x => x)
            .Take(3)
            .ToArray();

        return biggest[0] * biggest[1] * biggest[2];
    }

    private void DetermineBasin(GridWindow<int>[,] grid, GridWindow<int> window)
    {
        if (window.Tag is not null)
        {
            return;
        }
        if (window.Value == 9)
        {
            return;
        }
        var basin = new Basin() { Items = { window } };
        window.Tag = basin;
        foreach (var adjacentTile in window.AdjacentNonDiagonals())
        {
            if (adjacentTile.value is not 9 && adjacentTile.value < window.Value)
            {
                var adjacentWindow = grid[adjacentTile.x, adjacentTile.y];
                if (adjacentWindow.Tag == window.Tag)
                {
                    continue;
                }
                DetermineBasin(grid, adjacentWindow);
                var adjacentBasin = (Basin)adjacentWindow.Tag!;
                foreach (var item in basin.Items)
                {
                    adjacentBasin.Items.Add(item);
                }
                window.Tag = adjacentBasin;
            }
        }
    }
}
