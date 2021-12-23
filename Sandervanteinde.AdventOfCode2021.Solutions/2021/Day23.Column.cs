namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day23
{
    public readonly struct Column
    {
        public char FirstPosition { get; init; } = GameBoard.EMPTY;
        public char SecondPosition { get; init; } = GameBoard.EMPTY;

        public bool IsFull => this is { FirstPosition: not GameBoard.EMPTY, SecondPosition: not GameBoard.EMPTY };
        public bool IsEmpty => this is { FirstPosition: GameBoard.EMPTY, SecondPosition: GameBoard.EMPTY };

        public bool EmptyOrContainsOnly(char c)
        {
            return (FirstPosition == c || FirstPosition == GameBoard.EMPTY) && (SecondPosition == c || SecondPosition == GameBoard.EMPTY);
        }
    }
}