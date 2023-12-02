namespace Sandervanteinde.AdventOfCode.Solutions.Extensions;

internal static class TwoDimensionalArrayExtensions
{
    public static IEnumerable<T> EnumerateEntries<T>(this T[,] items)
    {
        var xLength = items.GetLength(dimension: 0);
        var yLength = items.GetLength(dimension: 1);

        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                var entry = items[x, y];
                yield return entry;
            }
        }
    }

    public static void VisualizeToConsole<T>(this T[,] items)
    {
        var xLength = items.GetLength(dimension: 0);
        var yLength = items.GetLength(dimension: 1);

        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                Console.Write(items[x, y]);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}
