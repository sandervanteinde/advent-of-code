namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day19
{
    private class Conversion
    {
        public string From { get; init; }
        public string To { get; init; }

        public override string ToString()
        {
            return $"{From} => {To}";
        }
    }
}
