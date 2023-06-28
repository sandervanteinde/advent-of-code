namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day06
{
    public class LightInstruction
    {
        public Point From { get; init; }
        public Point Through { get; init; }
        public LightOperation Operation { get; init; }

    }
}