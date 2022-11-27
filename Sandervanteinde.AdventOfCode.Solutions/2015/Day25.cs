using System.Text.RegularExpressions;
using Sandervanteinde.AdventOfCode.Solutions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal class Day25 : BaseSolution
{
    public Day25()
        : base("Let It Snow", 2015, 25)
    { }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var (row, column) = ParseRowAndColumn(reader);
        var id = DetermineId(row, column);

        var initValue = 20151125L;
        for (var i = 1; i < id; i++)
        {
            initValue = DetermineNextValue(initValue);
        }

        return initValue;
    }

    public static long DetermineNextValue(long currentValue)
    {
        checked
        {
            var newValue = currentValue * 252533L;
            return newValue % 33554393;
        }
    }

    public static long DetermineId(long row, long column)
    {
        var id = 1L;
        for (var i = 0; i < row; i++)
        {
            id += i;
        }
        for (var i = 1; i < column; i++)
        {
            id += (row + i);
        }
        return id;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        return "This is provided by Advent of Code 😃";
    }

    private static (long row, long column) ParseRowAndColumn(FileReader reader)
    {
        var regex = new Regex(@"row (\d+), column (\d+)");
        var match = regex.Match(reader.Input);
        if (!match.Success)
        {
            throw new InvalidOperationException("Invalid input");
        }

        return (long.Parse(match.Groups[1].Value), long.Parse(match.Groups[2].Value));
    }
}
