using System.Numerics;

namespace Sandervanteinde.AdventOfCode.Solutions.Extensions;

internal static class EnumerableExtensions
{
    public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
    {
        return new Queue<T>(enumerable);
    }

    public static T Product<T>(this IEnumerable<T> values)
        where T : INumber<T>
    {
        var i = T.One;

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
