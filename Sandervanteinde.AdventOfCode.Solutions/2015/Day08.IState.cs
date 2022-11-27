namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day08
{
    private interface IState
    {
        void Next(StringParser parser, OpMode mode);
    }
}
