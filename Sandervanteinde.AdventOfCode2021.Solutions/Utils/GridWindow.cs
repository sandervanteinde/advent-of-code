using System.Buffers;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Utils;

public class GridWindow<T> : IDisposable
{
    private static readonly ArrayPool<(int x, int y, T value)> pool = ArrayPool<(int x, int y, T value)>.Shared;
    private readonly T[,] grid;
    public object? Tag { get; set; }

    public int X { get; }
    public int Y { get; }

    public T Value => grid[X, Y];

    public bool IsCorner => (X is 0 && (Y == 0 || Y == grid.GetLength(1) - 1))
        || (X == grid.GetLength(0) - 1 && (Y == 0 || Y == grid.GetLength(1) - 1));

    private (int x, int y, T value)[] surroundings = Array.Empty<(int x, int y, T value)>();
    private int? amountOfSurroundings = null;

    private (int x, int y, T value)[] adjacents = Array.Empty<(int x, int y, T value)>();
    private int? amountOfAdjacents = null;

    public GridWindow(T[,] grid, int x, int y)
    {
        this.grid = grid;
        X = x;
        Y = y;
    }

    public Span<(int x, int y, T value)> AdjacentNonDiagonals()
    {
        if (!amountOfAdjacents.HasValue)
        {
            var array = pool.Rent(4);
            var amount = 0;
            if (X > 0)
            {
                array[amount++] = (X - 1, Y, grid[X - 1, Y]);
            }

            if (Y > 0)
            {
                array[amount++] = (X, Y - 1, grid[X, Y - 1]);
            }
            if ((Y + 1) < grid.GetLength(1))
            {
                array[amount++] = (X, Y + 1, grid[X, Y + 1]);
            }

            if ((X + 1) < grid.GetLength(0))
            {
                array[amount++] = (X + 1, Y, grid[X + 1, Y]);
            }

            adjacents = array;
            amountOfAdjacents = amount;
        }
        return adjacents.AsSpan(0, amountOfAdjacents.Value);
    }

    public Span<(int x, int y, T value)> Surroundings()
    {
        if (!amountOfSurroundings.HasValue)
        {
            var array = pool.Rent(8);
            var amount = 0;
            if (X > 0)
            {
                array[amount++] = (X - 1, Y, grid[X - 1, Y]);

                if (Y > 0)
                {
                    array[amount++] = (X - 1, Y - 1, grid[X - 1, Y - 1]);
                }
                if ((Y + 1) < grid.GetLength(1))
                {
                    array[amount++] = (X - 1, Y + 1, grid[X - 1, Y + 1]);
                }
            }

            if (Y > 0)
            {
                array[amount++] = (X, Y - 1, grid[X, Y - 1]);
            }
            if ((Y + 1) < grid.GetLength(1))
            {
                array[amount++] = (X, Y + 1, grid[X, Y + 1]);
            }

            if ((X + 1) < grid.GetLength(0))
            {
                array[amount++] = (X + 1, Y, grid[X + 1, Y]);

                if (Y > 0)
                {
                    array[amount++] = (X + 1, Y - 1, grid[X + 1, Y - 1]);
                }
                if ((Y + 1) < grid.GetLength(1))
                {
                    array[amount++] = (X + 1, Y + 1, grid[X + 1, Y + 1]);
                }
            }

            amountOfSurroundings = amount;
            surroundings = array;
        }
        return surroundings.AsSpan(0, amountOfSurroundings.Value);
    }


    public void Dispose()
    {
        if (surroundings.Length > 0)
        {
            pool.Return(surroundings);
        }
    }

    public static IEnumerable<GridWindow<T>> EnumerateGrid(T[,] grid)
    {
        var yLength = grid.GetLength(1);
        var xLength = grid.GetLength(0);
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                using var window = new GridWindow<T>(grid, x, y);
                yield return window;
            }
        }
    }

}
