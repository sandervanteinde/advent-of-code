namespace Sandervanteinde.AdventOfCode.Solutions._2016;

internal class Day03 : BaseSolution
{
    public Day03()
        : base("Squares With Three Sides", year: 2016, day: 3)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        return ParseInputsHorizontally(reader)
            .Where(input => input.lowest + input.middle > input.highest)
            .Count();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        return ParseInputsVertically(reader)
            .Where(input => input.lowest + input.middle > input.highest)
            .Count();
    }

    private static IEnumerable<(int lowest, int middle, int highest)> ParseInputsHorizontally(FileReader reader)
    {
        foreach (var line in reader.ReadLineByLine())
        {
            var split = line.Split(separator: ' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .OrderBy(x => x)
                .ToArray();
            yield return (split[0], split[1], split[2]);
        }
    }

    private static IEnumerable<(int lowest, int middle, int highest)> ParseInputsVertically(FileReader reader)
    {
        var lines = reader.ReadLineByLine()
            .ToArray();

        for (var i = 0; i < lines.Length; i += 3)
        {
            var line1 = ParseLine(lines[i]);
            var line2 = ParseLine(lines[i + 1]);
            var line3 = ParseLine(lines[i + 2]);
            yield return ReturnInOrder(line1[0], line2[0], line3[0]);
            yield return ReturnInOrder(line1[1], line2[1], line3[1]);
            yield return ReturnInOrder(line1[2], line2[2], line3[2]);
        }

        static int[] ParseLine(string input)
        {
            return input.Split(separator: ' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }

        static (int, int, int) ReturnInOrder(int first, int second, int third)
        {
            var ordered = new[] { first, second, third }.OrderBy(x => x)
                .ToArray();
            return (ordered[0], ordered[1], ordered[2]);
        }
    }
}
