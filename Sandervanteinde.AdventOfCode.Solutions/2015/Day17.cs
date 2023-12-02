namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal class Day17 : BaseSolution
{
    public Day17()
        : base("No Such Thing as Too Much", year: 2015, day: 17)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var buckets = ParseBuckets(reader)
            .ToList();
        var filled = 0;
        var countThatMatch = 0;

        IterateRecursively(startAt: 0);

        return countThatMatch;

        void IterateRecursively(int startAt)
        {
            if (filled > 150)
            {
                return;
            }

            if (filled == 150)
            {
                countThatMatch++;
                return;
            }

            for (var i = startAt; i < buckets.Count; i++)
            {
                var bucketValue = buckets[i];
                filled += bucketValue;
                IterateRecursively(i + 1);
                filled -= bucketValue;
            }
        }
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var buckets = ParseBuckets(reader)
            .ToList();
        var filled = 0;
        var countThatMatchByBuckets = new Dictionary<int, int>();
        var bucketSize = 0;

        IterateRecursively(startAt: 0);

        return countThatMatchByBuckets.MinBy(kvp => kvp.Key)
            .Value;

        void IterateRecursively(int startAt)
        {
            if (filled > 150)
            {
                return;
            }

            if (filled == 150)
            {
                countThatMatchByBuckets[bucketSize] = countThatMatchByBuckets.TryGetValue(bucketSize, out var existingAmount)
                    ? existingAmount + 1
                    : 1;
                return;
            }

            for (var i = startAt; i < buckets.Count; i++)
            {
                var bucketValue = buckets[i];
                filled += bucketValue;
                bucketSize++;
                IterateRecursively(i + 1);
                filled -= bucketValue;
                bucketSize--;
            }
        }
    }

    private static IEnumerable<int> ParseBuckets(FileReader reader)
    {
        return reader.ReadLineByLine()
            .Select(int.Parse);
    }
}
