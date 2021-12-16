namespace Sandervanteinde.AdventOfCode2021.Solutions.Extensions;

internal static class EnumerableExtensions
{
    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
    {
        return new(enumerable);
    }

    public static long Product(this IEnumerable<long> values)
    {
        var i = 1L;
        checked
        {
            foreach (var value in values)
            {
                i *= value;
            }
        }

        return i;
    }

    public static long Product<T>(this IEnumerable<T> values, Func<T, long> valueSelector)
    {
        return values
            .Select(valueSelector)
            .Product();
    }
}
