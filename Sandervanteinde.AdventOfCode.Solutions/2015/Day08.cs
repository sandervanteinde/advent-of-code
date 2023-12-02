namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day08 : BaseSolution
{
    public Day08()
        : base("Matchsticks", year: 2015, day: 8)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var rawStringLength = 0;
        var withoutQuotes = 0;

        foreach (var line in reader.ReadLineByLine())
        {
            var parser = new StringParser(line, OpMode.Read);
            var parsed = parser.GetResult();
            rawStringLength += line.Length;
            withoutQuotes += parsed.Length;
        }

        return rawStringLength - withoutQuotes;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var rawStringLength = 0;
        var withQuotes = 0;

        foreach (var line in reader.ReadLineByLine())
        {
            var parser = new StringParser(line, OpMode.Write);
            var parsed = parser.GetResult();
            rawStringLength += line.Length;
            withQuotes += parsed.Length;
        }

        return withQuotes - rawStringLength;
    }
}
