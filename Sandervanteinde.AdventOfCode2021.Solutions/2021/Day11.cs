using Sandervanteinde.AdventOfCode2021.Solutions.Extensions;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day11 : BaseSolution
{
    public Day11()
        : base("Dumbo Octopus", 2021, 11)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var grid = reader.ReadAsGrid(c => new Octopus(c - 48));
        var xLength = grid.GetLength(0);
        var yLength = grid.GetLength(1);
        var flashes = 0;
        var flashed = new Queue<GridWindow<Octopus>>();
        for (var i = 0; i < 100; i++)
        {
            foreach (var item in grid.EnumerateEntries())
            {
                item.Value.Increment();
            }
            foreach (var item in grid.EnumerateEntries())
            {
                if (item.Value.Flash())
                {
                    flashes++;
                    flashed.Enqueue(item);

                    while (flashed.Count > 0)
                    {
                        var adjacentFlashed = flashed.Dequeue();
                        foreach (var surrounding in adjacentFlashed.Surroundings())
                        {
                            if (surrounding.value.IncrementByFlash())
                            {
                                flashes++;
                                flashed.Enqueue(grid[surrounding.x, surrounding.y]);
                            }
                        }
                    }
                }
            }
        }
        return flashes;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var grid = reader.ReadAsGrid(c => new Octopus(c - 48));
        var xLength = grid.GetLength(0);
        var yLength = grid.GetLength(1);
        var flashed = new Queue<GridWindow<Octopus>>();
        var i = 0;
        do
        {
            foreach (var item in grid.EnumerateEntries())
            {
                item.Value.Increment();
            }
            foreach (var item in grid.EnumerateEntries())
            {
                if (item.Value.Flash())
                {
                    flashed.Enqueue(item);

                    while (flashed.Count > 0)
                    {
                        var adjacentFlashed = flashed.Dequeue();
                        foreach (var surrounding in adjacentFlashed.Surroundings())
                        {
                            if (surrounding.value.IncrementByFlash())
                            {
                                flashed.Enqueue(grid[surrounding.x, surrounding.y]);
                            }
                        }
                    }
                }
            }
            i++;
        }
        while (grid.EnumerateEntries().Any(x => x.Value.Value != 0));
        return i;
    }
}
