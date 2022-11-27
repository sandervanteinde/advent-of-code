using Sandervanteinde.AdventOfCode.Solutions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;
internal partial class Day07 : BaseSolution
{
    public Day07()
        : base("The Treachery of Whales", 2021, 7)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var crabs = reader.ReadCommaSeperatedNumbers()
            .ToArray();
        return DetermineResult(crabs, diff => diff);
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var horizontalPositions = reader.ReadCommaSeperatedNumbers()
            .ToArray();
        var lowestNum = horizontalPositions.Min();
        var highestNum = horizontalPositions.Max();
        var maxDiff = highestNum - lowestNum;
        var distance = 0;
        var distancePerDiff = new Dictionary<int, int>(maxDiff + 1);
        for (var i = 0; i <= maxDiff; i++)
        {
            distance += i;
            distancePerDiff[i] = distance;
        }
        return DetermineResult(horizontalPositions, diff => distancePerDiff[diff]);
    }

    private int DetermineResult(int[] crabs, Func<int, int> distanceCalculator)
    {
        var lowestNum = crabs.Min();
        var highestNum = crabs.Max();
        var maxDiff = highestNum - lowestNum;
        return Enumerable.Range(0, maxDiff)
             .Select(horizontalPosition => crabs.Sum(target => distanceCalculator(Math.Abs(horizontalPosition - target))))
             .Min(x => x);
    }
}
