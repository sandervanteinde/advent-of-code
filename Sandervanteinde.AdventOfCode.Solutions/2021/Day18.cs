namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day18 : BaseSolution
{
    public Day18()
        : base("Snailfish", year: 2021, day: 18)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        SnailfishBase? current = null;

        foreach (var snailFish in ParseSnailfishes(reader))
        {
            var valueToProcess = current is null
                ? snailFish
                : new SnailfishPair { Left = current, Right = snailFish };

            valueToProcess.Reduce();

            current = valueToProcess;
        }

        return current.Magnitude();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var snailFish = ParseSnailfishes(reader)
            .ToArray();
        var highest = long.MinValue;

        for (var i = 0; i < snailFish.Length; i++)
        {
            for (var j = i + 1; j < snailFish.Length; j++)
            {
                var left = snailFish[i];
                var right = snailFish[j];
                var pair = new SnailfishPair { Left = left.Clone(), Right = right.Clone() };
                pair.Reduce();
                highest = Math.Max(pair.Magnitude(), highest);
                pair = new SnailfishPair { Right = left.Clone(), Left = right.Clone() };
                pair.Reduce();
                highest = Math.Max(pair.Magnitude(), highest);
            }
        }

        return highest;
    }

    private static IEnumerable<SnailfishBase> ParseSnailfishes(FileReader reader)
    {
        foreach (var line in reader.ReadLineByLine())
        {
            yield return SnailfishBase.Parse(line);
        }
    }
}
