namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day19
{
    private class Conversion
    {
        public string From { get; init; } = null!;
        public string To { get; init; } = null!;

        public override string ToString()
        {
            return $"{From} => {To}";
        }
    }
}
