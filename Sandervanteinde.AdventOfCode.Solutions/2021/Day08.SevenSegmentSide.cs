namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day08
{
    [Flags]
    private enum SevenSegmentSide
    {
        None = 0,
        TopLeft = 0b0000001,
        Top = 0b0000010,
        TopRight = 0b0000100,
        Middle = 0b0001000,
        BottomLeft = 0b0010000,
        Bottom = 0b0100000,
        BottomRight = 0b1000000,

        All = 0b1111111,
        Right = TopRight | BottomRight,

        Zero = All & ~Middle,
        One = Right,
        Two = Top | TopRight | Middle | BottomLeft | Bottom,
        Three = Top | TopRight | Middle | BottomRight | Bottom,
        Four = TopLeft | TopRight | Middle | BottomRight,
        Five = Top | TopLeft | Middle | BottomRight | Bottom,
        Six = All & ~TopRight,
        Seven = Top | TopRight | BottomRight,
        Eight = All,
        Nine = All & ~BottomLeft
    }
}
