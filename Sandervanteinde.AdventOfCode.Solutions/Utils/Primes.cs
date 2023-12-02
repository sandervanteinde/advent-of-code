using System.Buffers;

namespace Sandervanteinde.AdventOfCode.Solutions.Utils;

public static class Primes
{
    public static IEnumerable<long> FactorsOf(long value)
    {
        while (value % 2 == 0)
        {
            yield return 2;
            value /= 2;
        }

        var sqrt = Math.Sqrt(value);

        for (var i = 3; i < sqrt; i += 2)
        {
            while (value % i == 0)
            {
                yield return i;
                value /= i;
            }
        }

        if (value > 2)
        {
            yield return value;
        }
    }

    public static IEnumerable<int> Enumerate(int max)
    {
        var sieve = ArrayPool<bool>.Shared.Rent(max);

        try
        {
            var actualMax = Convert.ToInt32(Math.Sqrt(max));

            for (var i = 2; i <= actualMax; i++)
            {
                if (!sieve[i])
                {
                    var j = 2;
                    var jIndex = j * i;

                    while (jIndex < max)
                    {
                        sieve[jIndex] = true;
                        jIndex = ++j * i;
                    }
                }
            }

            for (var i = 2; i < max; i++)
            {
                if (!sieve[i])
                {
                    yield return i;
                }
            }
        }
        finally
        {
            ArrayPool<bool>.Shared.Return(sieve);
        }
    }
}
