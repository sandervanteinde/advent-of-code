using System.Buffers;
using Sandervanteinde.AdventOfCode.Solutions.Extensions;

namespace Sandervanteinde.AdventOfCode.Solutions._2023;

internal class Day03 : BaseSolution
{
    private static readonly SearchValues<char> nonNumerics = SearchValues.Create("@&*$-#=%+/");
    private static readonly SearchValues<char> numerics = SearchValues.Create("0123456789");
    public Day03()
        : base("Gear ratios", year: 2023, day: 3)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var grid = reader.ReadAsGrid(c => c);
        var sum = 0;

        foreach (var entry in grid.EnumerateEntries())
        {
            var value = 0;
            GridWindow<char>? current = entry;
            var hasSymbolAdjacent = false;
            while (current is { Value: >= '0' and <= '9' } and { Tag: null })
            {
                current.Tag = true;
                value *= 10;
                value += current.Value - 48;
                hasSymbolAdjacent |= current.AdjacentWindowsIncludingDiagonals().Any(c => nonNumerics.Contains(c.Value));
                _ = current.TryGetValueAtRightSide(out current);
            }

            if (hasSymbolAdjacent)
            {
                sum += value;
            }
        }

        return sum;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var grid = reader.ReadAsGrid(c => c);
        var sum = 0;

        foreach (var tile in grid.EnumerateEntries())
        {
            if (tile.Value is not '*')
            {
                continue;
            }

            var itemsPerRow = tile.AdjacentWindowsIncludingDiagonals()
                .Where(c => numerics.Contains(c.Value))
                .ToHashSet();

            if (itemsPerRow.Count < 2)
            {
                continue;
            }

            var values = new List<int>();

            foreach (var item in itemsPerRow.ToArray())
            {
                if (!itemsPerRow.Remove(item))
                {
                    continue;
                }
                var value = item.Value - 48;
                var current = item;
                var exponent = 10;

                while (current.TryGetValueAtLeftSide(out current) && numerics.Contains(current.Value))
                {
                    value += (current.Value - 48) * exponent;
                    exponent *= 10;
                    itemsPerRow.Remove(current);
                }

                current = item;

                while (current.TryGetValueAtRightSide(out current) && numerics.Contains(current.Value))
                {
                    value *= 10;
                    value += current.Value - 48;
                    itemsPerRow.Remove(current);
                }

                values.Add(value);
            }

            if (values.Count != 2)
            {
                continue;
            }

            sum += (values[0] * values[1]);
        }

        return sum;
    }
}
