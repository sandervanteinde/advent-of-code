using FluentAssertions;
using Sandervanteinde.AdventOfCode2021.Solutions._2021;
using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using System;
using System.Collections.Generic;
using Xunit;
using static Sandervanteinde.AdventOfCode2021.Solutions._2021.Day23;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Tests._2021;

public partial class Day23Tests
{
    private readonly Day23 _sut;
    private readonly FileReader _reader;

    public Day23Tests()
    {
        _sut = new Day23();
        _reader = new FileReader(@"#############
#...........#
###B#C#B#D###
  #A#D#C#A#
  #########");
    }

    [Fact]
    public void StepOne_ShouldWorkWithExample()
    {
        _sut.DetermineStepOneResult(_reader)
            .Should().Be(12521L);
    }

    [Fact]
    public void StepTwo_ShouldWorkWithExample()
    {
        _sut.DetermineStepTwoResult(_reader)
            .Should().Be(44169);
    }

    [Fact]
    public void FollowingStepOneExampleSteps()
    {
        var exampleBoard = new GameBoardBuilder
        {
            ColumnOne = { FirstPosition = 'B', SecondPosition = 'A' },
            ColumnTwo = { FirstPosition = 'C', SecondPosition = 'D' },
            ColumnThree = { FirstPosition = 'B', SecondPosition = 'C' },
            ColumnFour = { FirstPosition = 'D', SecondPosition = 'A' }
        }.ToArray();
        var sum = 0L;
        PerformAndAdd(b => GameBoard.MoveColumnToLeft(b, 3));
        PerformAndAdd(b => GameBoard.MoveMiddleItemLeft(b, 3));
        sum.Should().Be(40);

        PerformAndAdd(b => GameBoard.MoveColumnToRight(b, 2));
        PerformAndAdd(b => GameBoard.MoveItemInFromLeft(b, 3));
        sum.Should().Be(40 + 400);

        PerformAndAdd(b => GameBoard.MoveColumnToRight(b, 2));
        PerformAndAdd(b => GameBoard.MoveItemInFromLeft(b, 2));
        sum.Should().Be(40 + 400 + 3000 + 30);

        PerformAndAdd(b => GameBoard.MoveColumnToRight(b, 1));
        PerformAndAdd(b => GameBoard.MoveItemInFromLeft(b, 2));
        sum.Should().Be(40 + 400 + 3000 + 30 + 40);

        PerformAndAdd(b => GameBoard.MoveColumnToLeft(b, 4));
        PerformAndAdd(b => GameBoard.MoveColumnToRight(b, 4));
        sum.Should().Be(40 + 400 + 3000 + 30 + 40 + 2003);

        PerformAndAdd(b => GameBoard.MoveItemInFromLeft(b, 4));
        PerformAndAdd(b => GameBoard.MoveItemInFromLeft(b, 4));
        sum.Should().Be(40 + 400 + 3000 + 30 + 40 + 2003 + 7000);

        PerformAndAdd(b => GameBoard.MoveItemInFromRight(b, 1));
        sum.Should().Be(12521);

        GameBoard.IsCorrect(exampleBoard).Should().BeTrue();

        void PerformAndAdd(Func<char[], (char[], long)> fn)
        {
            var old = GameBoard.AsString(exampleBoard);
            var (newBoard, score) = fn(exampleBoard);
            var @new = GameBoard.AsString(newBoard);
            sum += score;

            // check if this was one of the options
            var options = GameBoard.AllOptions(exampleBoard)
                .Should()
                .ContainEquivalentOf((newBoard, score));

            exampleBoard = newBoard;
        }
    }

    [Fact]
    public void FollowingSteptwoExampleSteps()
    {
        var exampleBoard = new char[]
        {
            '.', '.', '.', '.', '.', '.', '.',
                    'B','D', 'D', 'A',
                    'C', 'C', 'B', 'D',
                    'B', 'B', 'A', 'C',
                    'D', 'A', 'C', 'A'
        };

        long sum = 0;
        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, 4));
        PerformAndAdd(b => GameBoardLarge.MoveColumnToLeft(b, 4));
        PerformAndAdd(b => GameBoardLarge.MoveMiddleItemLeft(b, 4));
        PerformAndAdd(b => GameBoardLarge.MoveMiddleItemLeft(b, 3));
        PerformAndAdd(b => GameBoardLarge.MoveMiddleItemLeft(b, 2));
        long expectedSum = 2000 + 9;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, 3));
        PerformAndAdd(b => GameBoardLarge.MoveMiddleItemRight(b, 4));
        expectedSum += 1000 + 40;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, 3));
        expectedSum += 30;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToLeft(b, 3));
        PerformAndAdd(b => GameBoardLarge.MoveMiddleItemLeft(b, 3));
        PerformAndAdd(b => GameBoardLarge.MoveMiddleItemLeft(b, 2));
        expectedSum += 9;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, 2));
        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, 3));
        expectedSum += 600;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, 2));
        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, 3));
        expectedSum += 600;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, 2));
        expectedSum += 40;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToLeft(b, 2));
        expectedSum += 5000;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveItemInFromRight(b, 2));
        PerformAndAdd(b => GameBoardLarge.MoveItemInFromRight(b, 2));
        PerformAndAdd(b => GameBoardLarge.MoveItemInFromRight(b, 2));
        expectedSum += 180;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToLeft(b, 4));
        PerformAndAdd(b => GameBoardLarge.MoveItemInFromRight(b, 3));
        expectedSum += 600;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, 4));
        expectedSum += 5;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, 4));
        expectedSum += 9000;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, 1));
        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, 2));
        expectedSum += 40;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, 1));
        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, 4));
        expectedSum += 11000;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, 1));
        expectedSum += 4000;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, 1));
        expectedSum += 4;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, 4));
        expectedSum += 7000;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, 1));
        expectedSum += 4;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveItemInFromRight(b, 1));
        expectedSum += 8;
        sum.Should().Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveItemInFromRight(b, 4));
        expectedSum += 3000;
        sum.Should().Be(expectedSum);

        sum.Should().Be(44169);
        GameBoardLarge.IsCorrect(exampleBoard).Should().BeTrue();

        void PerformAndAdd(Func<char[], (char[], long)> fn)
        {
            var old = GameBoardLarge.AsString(exampleBoard);
            var (newBoard, score) = fn(exampleBoard);
            var @new = GameBoardLarge.AsString(newBoard);
            sum += score;

            // check if this was one of the options
            var options = GameBoardLarge.AllOptions(exampleBoard)
                .Should()
                .ContainEquivalentOf((newBoard, score));

            exampleBoard = newBoard;
        }
    }


    public class TestScenario
    {
        internal GameBoardBuilder Start { get; init; } = new();
        public int LaneToMove { get; init; }
        internal GameBoardBuilder? Expected { get; init; }
        public int ExpectedScore { get; init; }
    }

    public static IEnumerable<object[]> MoveLeftScenarios => new object[][]
    {
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { ColumnOne = { FirstPosition = 'A' }, LeftColumn = { FirstPosition = 'B', SecondPosition = 'C' } },
                LaneToMove = 1,
                Expected = null,
                ExpectedScore = 0
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { LeftColumn = { FirstPosition = 'A', SecondPosition = 'A'}, BetweenOneAndTwo = 'A', BetweenTwoAndThree = 'A', BetweenThreeAndFour = 'A', ColumnFour = new() { SecondPosition = 'A' } },
                LaneToMove = 4,
                Expected = null
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { LeftColumn = new(){ FirstPosition = 'B' }, BetweenOneAndTwo = 'C', BetweenTwoAndThree = 'D', BetweenThreeAndFour = 'A', ColumnFour = new() { SecondPosition = 'C' } },
                LaneToMove = 4,
                Expected = new GameBoardBuilder {LeftColumn = new() { FirstPosition = 'C', SecondPosition = 'B'}, BetweenOneAndTwo = 'D', BetweenTwoAndThree = 'A', BetweenThreeAndFour = 'C' },
                ExpectedScore = 10 + 200 + 2000 + 2 + 300
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder{ BetweenOneAndTwo = 'B', ColumnTwo = { FirstPosition = 'A', SecondPosition = 'B' }  },
                LaneToMove = 2,
                Expected = new GameBoardBuilder { LeftColumn = { FirstPosition = 'B' }, BetweenOneAndTwo = 'A', ColumnTwo = {SecondPosition = 'B' } },
                ExpectedScore = 22
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder{ColumnOne =  { FirstPosition = 'A', SecondPosition = 'B' } },
                LaneToMove = 1,
                Expected = new GameBoardBuilder{LeftColumn = {FirstPosition = 'A'}, ColumnOne = { SecondPosition = 'B' } },
                ExpectedScore = 2
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { ColumnTwo = { SecondPosition  = 'B' } },
                LaneToMove = 2,
                Expected = new GameBoardBuilder { BetweenOneAndTwo = 'B' },
                ExpectedScore = 30
            }
        }
    };

    [Theory]
    [MemberData(nameof(MoveLeftScenarios))]
    public void GameBoard_MoveLeftScenarios(TestScenario testScenario)
    {
        var start = testScenario.Start.ToArray();
        var result = GameBoard.MoveColumnToLeft(start, testScenario.LaneToMove);

        result.Should().BeEquivalentTo((testScenario.Expected?.ToArray() ?? Array.Empty<char>(), testScenario.ExpectedScore));
    }

    public static IEnumerable<object[]> MoveRightScenarios => new object[][]
    {
        new object[]
        {
            new TestScenario
            {
                Start = new() { ColumnOne = new() { FirstPosition = 'B' }, LeftColumn =  { FirstPosition = 'C', SecondPosition = 'B' }, BetweenOneAndTwo = 'A', BetweenTwoAndThree = 'D', BetweenThreeAndFour = 'C', RightColumn = new() { FirstPosition = 'A', SecondPosition = 'D' } },
                LaneToMove = 1,
                Expected = null,
                ExpectedScore = 0
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new() { ColumnTwo =  { FirstPosition = 'A' } },
                LaneToMove = 2,
                Expected = new() { BetweenTwoAndThree = 'A' },
                ExpectedScore = 2
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new() { ColumnFour =  { FirstPosition = 'C' }, RightColumn =  { FirstPosition = 'A' }},
                LaneToMove = 4,
                Expected = new (){ RightColumn =  { FirstPosition = 'C', SecondPosition = 'A'}},
                ExpectedScore = 201
            },
        }
    };

    [Theory]
    [MemberData(nameof(MoveRightScenarios))]
    public void GameBoard_MoveRightScenarios(TestScenario testScenario)
    {
        var start = testScenario.Start.ToArray();
        var result = GameBoard.MoveColumnToRight(start, testScenario.LaneToMove);

        result.Should().BeEquivalentTo((testScenario.Expected?.ToArray() ?? Array.Empty<char>(), testScenario.ExpectedScore));
    }

    public static IEnumerable<object[]> MoveItemInFromLeftScenarios = new object[][]
    {
        new object[]
        {
            new TestScenario
            {
                Start = new() { LeftColumn =  { SecondPosition = 'C' }, ColumnThree = { SecondPosition = 'C' } },
                LaneToMove = 3,
                Expected = new() { ColumnThree =  { FirstPosition = 'C', SecondPosition = 'C' } },
                ExpectedScore = 700
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new() { RightColumn = { FirstPosition = 'A', SecondPosition = 'A' } },
                LaneToMove = 1,
                Expected = null,
                ExpectedScore = 0
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new() { LeftColumn =  { SecondPosition = 'C' } },
                LaneToMove = 3,
                Expected = new() { ColumnThree =  { SecondPosition = 'C' } },
                ExpectedScore = 800
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new() { LeftColumn =  { SecondPosition = 'A' }, ColumnOne =  { SecondPosition = 'B' } },
                LaneToMove = 1,
                Expected = null,
                ExpectedScore = 0
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new() { LeftColumn =  { FirstPosition = 'A' } },
                LaneToMove = 1,
                Expected = new() { ColumnOne =  { SecondPosition = 'A' } },
                ExpectedScore = 3
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new () { LeftColumn =  { SecondPosition = 'A' } },
                LaneToMove = 1,
                Expected = new () { ColumnOne =  { SecondPosition = 'A' } },
                ExpectedScore = 4
            }
        }
    };

    [Theory]
    [MemberData(nameof(MoveItemInFromLeftScenarios))]
    public void GameBoard_MoveItemInFromLeft(TestScenario testScenario)
    {
        var result = GameBoard.MoveItemInFromLeft(testScenario.Start.ToArray(), testScenario.LaneToMove);

        result.Should().BeEquivalentTo((testScenario.Expected?.ToArray() ?? Array.Empty<char>(), testScenario.ExpectedScore));
    }
    public static IEnumerable<object[]> MoveItemInFromRightScenarios = new object[][]
    {
        new object[]
        {
            new TestScenario
            {
                Start = new () { RightColumn =  { SecondPosition = 'C' } },
                LaneToMove = 3,
                Expected = new () { ColumnThree =  { SecondPosition = 'C' } },
                ExpectedScore = 600
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new () { RightColumn =  { SecondPosition = 'D' } },
                LaneToMove = 4,
                Expected = new () { ColumnFour =  { SecondPosition = 'D' } },
                ExpectedScore = 4000
            }
        }
    };
    [Theory]
    [MemberData(nameof(MoveItemInFromRightScenarios))]
    public void GameBoard_MoveItemInFromRight(TestScenario testScenario)
    {
        var result = GameBoard.MoveItemInFromRight(testScenario.Start.ToArray(), testScenario.LaneToMove);

        result.Should().BeEquivalentTo((testScenario.Expected?.ToArray() ?? Array.Empty<char>(), testScenario.ExpectedScore));
    }

    public static object[][] MoveMiddleItemLeftScenarios => new object[][]
    {
        new object[]
        {
            new TestScenario
            {
                Start = new () { LeftColumn = new() { FirstPosition = 'B' }, BetweenOneAndTwo = 'A', BetweenThreeAndFour = 'C' },
                LaneToMove = 4,
                Expected = new () { LeftColumn = new() { FirstPosition = 'B' }, BetweenOneAndTwo = 'A', BetweenTwoAndThree = 'C' },
                ExpectedScore = 200
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new () { LeftColumn = { FirstPosition = 'B' }, BetweenOneAndTwo = 'A', BetweenTwoAndThree = 'B', BetweenThreeAndFour = 'C' },
                LaneToMove = 4,
                Expected = new() { LeftColumn = { SecondPosition = 'B', FirstPosition = 'A'}, BetweenOneAndTwo = 'B', BetweenTwoAndThree = 'C'},
                ExpectedScore = 10 + 2 + 20 + 200
            },
        },
        new object[]
        {
            new TestScenario
            {
                Start = new () { BetweenOneAndTwo = 'B' },
                LaneToMove = 2,
                Expected = new() { LeftColumn = { FirstPosition = 'B'}},
                ExpectedScore = 20
            }
        }
    };

    [Theory]
    [MemberData(nameof(MoveMiddleItemLeftScenarios))]
    public void GameBoard_MoveMiddleItemLeft(TestScenario scenario)
    {
        GameBoard.MoveMiddleItemLeft(scenario.Start.ToArray(), scenario.LaneToMove)
            .Should().BeEquivalentTo((scenario.Expected.ToArray(), scenario.ExpectedScore));
    }
    public static object[][] MoveMiddleItemRightScenarios => new object[][]
    {
        new object[]
        {
            new TestScenario
            {
                Start = new () { BetweenOneAndTwo = 'A', BetweenTwoAndThree = 'B', BetweenThreeAndFour = 'C', RightColumn = { FirstPosition = 'B' } },
                LaneToMove = 2,
                Expected = new() { BetweenTwoAndThree = 'A', BetweenThreeAndFour = 'B', RightColumn = { FirstPosition = 'C', SecondPosition = 'B'} },
                ExpectedScore = 10 + 2 + 20 + 200
            },
        },
        new object[]
        {
            new TestScenario
            {
                Start = new () { BetweenOneAndTwo = 'A', BetweenThreeAndFour = 'C' },
                LaneToMove = 4,
                Expected = new () { BetweenOneAndTwo = 'A', RightColumn = new() { FirstPosition = 'C' } },
                ExpectedScore = 200
            }
        }
    };

    [Theory]
    [MemberData(nameof(MoveMiddleItemRightScenarios))]
    public void GameBoard_MoveMiddleItemRight(TestScenario scenario)
    {
        GameBoard.MoveMiddleItemRight(scenario.Start.ToArray(), scenario.LaneToMove)
            .Should().BeEquivalentTo((scenario.Expected.ToArray(), scenario.ExpectedScore));
    }

}