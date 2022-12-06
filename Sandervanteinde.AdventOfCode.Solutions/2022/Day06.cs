using System.Diagnostics;

namespace Sandervanteinde.AdventOfCode.Solutions._2022;

internal class Day06 : BaseSolution
{
    public Day06()
        : base("Tuning Trouble", 2022, 6)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var span = reader.Input.AsSpan();
        for(var i = 4; i < span.Length; i++)
        {
            var slice = span[(i - 4)..i];
            if(slice.ToArray().Distinct().Count() == 4)
            {
                return i;
            }

        }

        return -1;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var span = reader.Input.AsSpan();
        for (var i = 14; i < span.Length; i++)
        {
            var slice = span[(i - 14)..i];
            if (slice.ToArray().Distinct().Count() == 14)
            {
                return i;
            }

        }

        return -1;
    }
}
