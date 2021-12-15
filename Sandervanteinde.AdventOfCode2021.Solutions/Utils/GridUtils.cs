namespace Sandervanteinde.AdventOfCode2021.Solutions.Utils;

internal static class GridUtils
{
    public static IEnumerable<T> EnumerateGrid<T>(T[,] grid)
    {
        var xLength = grid.GetLength(0);
        var yLength = grid.GetLength(1);
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                yield return grid[x, y];
            }
        }
    }

    public static GridWindow<T>[,] ToGridwindows<T>(this T[,] grid)
    {
        var xLength = grid.GetLength(0);
        var yLength = grid.GetLength(1);
        var gridWindows = new GridWindow<T>[xLength, yLength];
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                gridWindows[x, y] = new GridWindow<T>(grid, x, y);
            }
        }
        return gridWindows;
    }
}
