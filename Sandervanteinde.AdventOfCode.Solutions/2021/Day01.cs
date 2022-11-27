namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal class Day01 : BaseSolution
{
    public Day01()
        : base("Sonar Sweep", 2021, 1)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var counter = 0;
        foreach (var pair in Pairwise(ParseInput(reader), 2))
        {
            if (pair[1] > pair[0])
            {
                counter++;
            }
        }
        return counter;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var previousSum = int.MaxValue;
        var counter = 0;
        foreach (var pair in Pairwise(ParseInput(reader), 3))
        {
            var sum = pair.Sum();
            if (sum > previousSum)
            {
                ++counter;
            }
            previousSum = sum;
        }
        return counter;
    }

    private static IEnumerable<int[]> Pairwise(IEnumerable<int> enumerable, int size)
    {
        var queue = new Queue<int>(size);
        foreach (var item in enumerable)
        {
            if (queue.Count == size)
            {
                queue.Dequeue();
            }
            queue.Enqueue(item);
            if (queue.Count == size)
            {
                yield return queue.ToArray();
            }
        }
    }

    private static IEnumerable<int> ParseInput(FileReader reader)
    {
        return reader.ReadLineByLine().Select(int.Parse);
    }
}
