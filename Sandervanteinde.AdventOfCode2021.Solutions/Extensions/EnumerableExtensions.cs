namespace Sandervanteinde.AdventOfCode2021.Solutions.Extensions;

internal static class EnumerableExtensions
{
    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
    {
        return new(enumerable);
    }
}
