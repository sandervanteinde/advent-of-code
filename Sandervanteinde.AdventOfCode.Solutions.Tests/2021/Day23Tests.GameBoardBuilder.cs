﻿namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2021;

public partial class Day23Tests
{
    public class GameBoardBuilder
    {
        public ColumnBuilder ColumnFour = new();
        public ColumnBuilder ColumnOne = new();
        public ColumnBuilder ColumnThree = new();
        public ColumnBuilder ColumnTwo = new();
        public ColumnBuilder LeftColumn = new();
        public ColumnBuilder RightColumn = new();
        public char BetweenOneAndTwo { get; set; } = '.';
        public char BetweenTwoAndThree { get; set; } = '.';
        public char BetweenThreeAndFour { get; set; } = '.';

        public char[] ToArray()
        {
            return new[]
            {
                LeftColumn.SecondPosition, LeftColumn.FirstPosition, BetweenOneAndTwo, BetweenTwoAndThree, BetweenThreeAndFour, RightColumn.FirstPosition,
                RightColumn.SecondPosition, ColumnOne.FirstPosition, ColumnOne.SecondPosition, ColumnTwo.FirstPosition, ColumnTwo.SecondPosition,
                ColumnThree.FirstPosition, ColumnThree.SecondPosition, ColumnFour.FirstPosition, ColumnFour.SecondPosition
            };
        }
    }
}
