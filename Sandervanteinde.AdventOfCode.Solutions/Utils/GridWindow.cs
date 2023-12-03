using System.Buffers;
using System.Diagnostics.CodeAnalysis;

namespace Sandervanteinde.AdventOfCode.Solutions.Utils;

public class GridWindow<T> : IDisposable
{
    private static readonly ArrayPool<(int x, int y, T value)> pool = ArrayPool<(int x, int y, T value)>.Shared;
    private readonly GridWindow<T>[,] grid;
    private readonly T[,] values;

    private (int x, int y, T value)[] adjacents = Array.Empty<(int x, int y, T value)>();
    private int? amountOfAdjacents;
    private int? amountOfSurroundings;

    private (int x, int y, T value)[] surroundings = Array.Empty<(int x, int y, T value)>();

    public GridWindow(GridWindow<T>[,] grid, T[,] values, int x, int y)
    {
        this.grid = grid;
        this.values = values;
        X = x;
        Y = y;
    }

    public object? Tag { get; set; }

    public int X { get; }
    public int Y { get; }
    public bool IsTopRow => Y is 0;
    public bool IsBottomRow => (Y + 1) == grid.GetLength(1);
    public bool IsLeftColumn => X is 0;
    public bool IsRightColumn => (X + 1) == grid.GetLength(0);

    public GridWindow<T> TopLeft => grid[X - 1, Y - 1];
    public GridWindow<T> Top => grid[X, Y - 1];
    public GridWindow<T> TopRight => grid[X + 1, Y - 1];
    public GridWindow<T> Left => grid[X - 1, Y];
    public GridWindow<T> Right => grid[X + 1, Y];
    public GridWindow<T> BottomLeft => grid[X - 1, Y + 1];
    public GridWindow<T> Bottom => grid[X, Y + 1];
    public GridWindow<T> BottomRight => grid[X + 1, Y + 1];

    public T Value => values[X, Y];

    public bool IsCorner => (X is 0 && (Y == 0 || Y == grid.GetLength(dimension: 1) - 1))
        || (X == grid.GetLength(dimension: 0) - 1 && (Y == 0 || Y == grid.GetLength(dimension: 1) - 1));

    public void Dispose()
    {
        if (surroundings.Length > 0)
        {
            pool.Return(surroundings);
        }
    }

    public IEnumerable<GridWindow<T>> AdjacentWindows()
    {
        if (X > 0)
        {
            yield return grid[X - 1, Y];
        }

        if (Y > 0)
        {
            yield return grid[X, Y - 1];
        }

        if (Y + 1 < grid.GetLength(dimension: 1))
        {
            yield return grid[X, Y + 1];
        }

        if (X + 1 < grid.GetLength(dimension: 0))
        {
            yield return grid[X + 1, Y];
        }
    }

    public IEnumerable<GridWindow<T>> AdjacentWindowsIncludingDiagonals()
    {
        if (!IsLeftColumn)
        {
            if (!IsTopRow)
            {
                yield return TopLeft;
            }

            yield return Left;

            if (!IsBottomRow)
            {
                yield return BottomLeft;
            }
        }

        if (!IsTopRow)
        {
            yield return Top;
        }

        if (!IsBottomRow)
        {
            yield return Bottom;
        }

        if (IsRightColumn)
        {
            yield break;
        }

        if (!IsTopRow)
        {
            yield return TopRight;
        }

        yield return Right;

        if (!IsBottomRow)
        {
            yield return BottomRight;
        }
    }

    public Span<(int x, int y, T value)> AdjacentNonDiagonals()
    {
        if (!amountOfAdjacents.HasValue)
        {
            var array = pool.Rent(minimumLength: 4);
            var amount = 0;

            if (X > 0)
            {
                array[amount++] = (X - 1, Y, grid[X - 1, Y].Value);
            }

            if (Y > 0)
            {
                array[amount++] = (X, Y - 1, grid[X, Y - 1].Value);
            }

            if (Y + 1 < grid.GetLength(dimension: 1))
            {
                array[amount++] = (X, Y + 1, grid[X, Y + 1].Value);
            }

            if (X + 1 < grid.GetLength(dimension: 0))
            {
                array[amount++] = (X + 1, Y, grid[X + 1, Y].Value);
            }

            adjacents = array;
            amountOfAdjacents = amount;
        }

        return adjacents.AsSpan(start: 0, amountOfAdjacents.Value);
    }

    public Span<(int x, int y, T value)> Surroundings(bool includeSelf = false)
    {
        if (!amountOfSurroundings.HasValue)
        {
            var array = pool.Rent(minimumLength: 9);
            var amount = 0;

            if (X > 0)
            {
                array[amount++] = (X - 1, Y, grid[X - 1, Y].Value);

                if (Y > 0)
                {
                    array[amount++] = (X - 1, Y - 1, grid[X - 1, Y - 1].Value);
                }

                if (Y + 1 < grid.GetLength(dimension: 1))
                {
                    array[amount++] = (X - 1, Y + 1, grid[X - 1, Y + 1].Value);
                }
            }

            if (Y > 0)
            {
                array[amount++] = (X, Y - 1, grid[X, Y - 1].Value);
            }

            if (includeSelf)
            {
                array[amount++] = (X, Y, Value);
            }

            if (Y + 1 < grid.GetLength(dimension: 1))
            {
                array[amount++] = (X, Y + 1, grid[X, Y + 1].Value);
            }

            if (X + 1 < grid.GetLength(dimension: 0))
            {
                array[amount++] = (X + 1, Y, grid[X + 1, Y].Value);

                if (Y > 0)
                {
                    array[amount++] = (X + 1, Y - 1, grid[X + 1, Y - 1].Value);
                }

                if (Y + 1 < grid.GetLength(dimension: 1))
                {
                    array[amount++] = (X + 1, Y + 1, grid[X + 1, Y + 1].Value);
                }
            }

            amountOfSurroundings = amount;
            surroundings = array;
        }

        return surroundings.AsSpan(start: 0, amountOfSurroundings.Value);
    }

    public static IEnumerable<GridWindow<T>> EnumerateGrid(T[,] grid)
    {
        var yLength = grid.GetLength(dimension: 1);
        var xLength = grid.GetLength(dimension: 0);
        var windows = grid.ToGridwindows();

        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                using var window = new GridWindow<T>(windows, grid, x, y);
                yield return window;
            }
        }
    }

    public override string? ToString()
    {
        return Value?.ToString();
    }

    public bool TryGetValueAtRightSide([NotNullWhen(returnValue: true)] out GridWindow<T>? o)
    {
        if (!IsRightColumn)
        {
            o = Right;
            return true;
        }

        o = default!;
        return false;
    }

    public bool TryGetValueAtLeftSide([NotNullWhen(returnValue: true)] out GridWindow<T>? o)
    {
        if (!IsLeftColumn)
        {
            o = Left;
            return true;
        }

        o = default!;
        return false;
    }
}
