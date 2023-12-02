namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal class Day24 : BaseSolution
{
    public Day24()
        : base("It Hangs in the Balance", year: 2015, day: 24)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var packageValues = ParsePackageValues(reader)
            .ToArray();
        var totalWeight = packageValues.Sum();
        var packageWeightPerSection = totalWeight / 3;
        var list = new LinkedList<long>(packageValues);
        var combinationsThatMakePackageWeight = CombinationsThatMakeSize(list.First, packageWeightPerSection, new Stack<long>(), currentSum: 0)
            .ToArray();
        var smallestGroup = combinationsThatMakePackageWeight
                .GroupBy(group => group.Count())
                .MinBy(group => group.Key)
                ?.Min(Product)
            ?? throw new InvalidOperationException("Should have found something");

        return smallestGroup;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var packageValues = ParsePackageValues(reader)
            .ToArray();
        var totalWeight = packageValues.Sum();
        var packageWeightPerSection = totalWeight / 4;
        var list = new LinkedList<long>(packageValues);
        var combinationsThatMakePackageWeight = CombinationsThatMakeSize(list.First, packageWeightPerSection, new Stack<long>(), currentSum: 0)
            .ToArray();
        var smallestGroup = combinationsThatMakePackageWeight
                .GroupBy(group => group.Count())
                .MinBy(group => group.Key)
                ?.Min(Product)
            ?? throw new InvalidOperationException("Should have found something");

        return smallestGroup;
    }

    private long Product(IEnumerable<long> values)
    {
        checked
        {
            var product = 1L;

            foreach (var value in values)
            {
                product *= value;
            }

            return product;
        }
    }

    private IEnumerable<long[]> CombinationsThatMakeSize(LinkedListNode<long>? node, long size, Stack<long> set, long currentSum)
    {
        while (node is not null)
        {
            set.Push(node.Value);
            var newSum = currentSum + node.Value;

            if (newSum > size)
            {
                set.Pop();
                break;
            }

            if (newSum == size)
            {
                yield return set.ToArray();
                set.Pop();
                break;
            }

            foreach (var inner in CombinationsThatMakeSize(node.Next, size, set, newSum))
            {
                yield return inner;
            }

            set.Pop();
            node = node.Next;
        }
    }

    private IEnumerable<long> ParsePackageValues(FileReader reader)
    {
        return reader.ReadLineByLine()
            .Select(long.Parse);
    }
}
