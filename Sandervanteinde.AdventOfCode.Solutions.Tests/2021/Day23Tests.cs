using System;
using System.Collections.Generic;
using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions._2021;
using Sandervanteinde.AdventOfCode.Solutions.Utils;
using Xunit;
using static Sandervanteinde.AdventOfCode.Solutions._2021.Day23;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2021;

public partial class Day23Tests
{
    public static IEnumerable<object[]> MoveItemInFromLeftScenarios = new[]
    {
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { LeftColumn = { SecondPosition = 'C' }, ColumnThree = { SecondPosition = 'C' } },
                LaneToMove = 3,
                Expected = new GameBoardBuilder { ColumnThree = { FirstPosition = 'C', SecondPosition = 'C' } },
                ExpectedScore = 700
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { RightColumn = { FirstPosition = 'A', SecondPosition = 'A' } },
                LaneToMove = 1,
                Expected = null,
                ExpectedScore = 0
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { LeftColumn = { SecondPosition = 'C' } },
                LaneToMove = 3,
                Expected = new GameBoardBuilder { ColumnThree = { SecondPosition = 'C' } },
                ExpectedScore = 800
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { LeftColumn = { SecondPosition = 'A' }, ColumnOne = { SecondPosition = 'B' } },
                LaneToMove = 1,
                Expected = null,
                ExpectedScore = 0
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { LeftColumn = { FirstPosition = 'A' } },
                LaneToMove = 1,
                Expected = new GameBoardBuilder { ColumnOne = { SecondPosition = 'A' } },
                ExpectedScore = 3
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { LeftColumn = { SecondPosition = 'A' } },
                LaneToMove = 1,
                Expected = new GameBoardBuilder { ColumnOne = { SecondPosition = 'A' } },
                ExpectedScore = 4
            }
        }
    };

    public static IEnumerable<object[]> MoveItemInFromRightScenarios = new[]
    {
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { RightColumn = { SecondPosition = 'C' } },
                LaneToMove = 3,
                Expected = new GameBoardBuilder { ColumnThree = { SecondPosition = 'C' } },
                ExpectedScore = 600
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { RightColumn = { SecondPosition = 'D' } },
                LaneToMove = 4,
                Expected = new GameBoardBuilder { ColumnFour = { SecondPosition = 'D' } },
                ExpectedScore = 4000
            }
        }
    };

    private readonly FileReader _reader;
    private readonly Day23 _sut;

    public Day23Tests()
    {
        _sut = new Day23();
        _reader = new FileReader(
            @"#############
#...........#
###B#C#B#D###
  #A#D#C#A#
  #########"
        );
    }

    public static IEnumerable<object[]> MoveLeftScenarios => new[]
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
                Start = new GameBoardBuilder
                {
                    LeftColumn = { FirstPosition = 'A', SecondPosition = 'A' },
                    BetweenOneAndTwo = 'A',
                    BetweenTwoAndThree = 'A',
                    BetweenThreeAndFour = 'A',
                    ColumnFour = new ColumnBuilder { SecondPosition = 'A' }
                },
                LaneToMove = 4,
                Expected = null
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder
                {
                    LeftColumn = new ColumnBuilder { FirstPosition = 'B' },
                    BetweenOneAndTwo = 'C',
                    BetweenTwoAndThree = 'D',
                    BetweenThreeAndFour = 'A',
                    ColumnFour = new ColumnBuilder { SecondPosition = 'C' }
                },
                LaneToMove = 4,
                Expected =
                    new GameBoardBuilder
                    {
                        LeftColumn = new ColumnBuilder { FirstPosition = 'C', SecondPosition = 'B' },
                        BetweenOneAndTwo = 'D',
                        BetweenTwoAndThree = 'A',
                        BetweenThreeAndFour = 'C'
                    },
                ExpectedScore = 10 + 200 + 2000 + 2 + 300
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { BetweenOneAndTwo = 'B', ColumnTwo = { FirstPosition = 'A', SecondPosition = 'B' } },
                LaneToMove = 2,
                Expected =
                    new GameBoardBuilder { LeftColumn = { FirstPosition = 'B' }, BetweenOneAndTwo = 'A', ColumnTwo = { SecondPosition = 'B' } },
                ExpectedScore = 22
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { ColumnOne = { FirstPosition = 'A', SecondPosition = 'B' } },
                LaneToMove = 1,
                Expected = new GameBoardBuilder { LeftColumn = { FirstPosition = 'A' }, ColumnOne = { SecondPosition = 'B' } },
                ExpectedScore = 2
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { ColumnTwo = { SecondPosition = 'B' } },
                LaneToMove = 2,
                Expected = new GameBoardBuilder { BetweenOneAndTwo = 'B' },
                ExpectedScore = 30
            }
        }
    };

    public static IEnumerable<object[]> MoveRightScenarios => new[]
    {
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder
                {
                    ColumnOne = new ColumnBuilder { FirstPosition = 'B' },
                    LeftColumn = { FirstPosition = 'C', SecondPosition = 'B' },
                    BetweenOneAndTwo = 'A',
                    BetweenTwoAndThree = 'D',
                    BetweenThreeAndFour = 'C',
                    RightColumn = new ColumnBuilder { FirstPosition = 'A', SecondPosition = 'D' }
                },
                LaneToMove = 1,
                Expected = null,
                ExpectedScore = 0
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { ColumnTwo = { FirstPosition = 'A' } },
                LaneToMove = 2,
                Expected = new GameBoardBuilder { BetweenTwoAndThree = 'A' },
                ExpectedScore = 2
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { ColumnFour = { FirstPosition = 'C' }, RightColumn = { FirstPosition = 'A' } },
                LaneToMove = 4,
                Expected = new GameBoardBuilder { RightColumn = { FirstPosition = 'C', SecondPosition = 'A' } },
                ExpectedScore = 201
            }
        }
    };

    public static object[][] MoveMiddleItemLeftScenarios => new[]
    {
        new object[]
        {
            new TestScenario
            {
                Start =
                    new GameBoardBuilder
                    {
                        LeftColumn = new ColumnBuilder { FirstPosition = 'B' }, BetweenOneAndTwo = 'A', BetweenThreeAndFour = 'C'
                    },
                LaneToMove = 4,
                Expected =
                    new GameBoardBuilder
                    {
                        LeftColumn = new ColumnBuilder { FirstPosition = 'B' }, BetweenOneAndTwo = 'A', BetweenTwoAndThree = 'C'
                    },
                ExpectedScore = 200
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start =
                    new GameBoardBuilder
                    {
                        LeftColumn = { FirstPosition = 'B' }, BetweenOneAndTwo = 'A', BetweenTwoAndThree = 'B', BetweenThreeAndFour = 'C'
                    },
                LaneToMove = 4,
                Expected =
                    new GameBoardBuilder
                    {
                        LeftColumn = { SecondPosition = 'B', FirstPosition = 'A' }, BetweenOneAndTwo = 'B', BetweenTwoAndThree = 'C'
                    },
                ExpectedScore = 10 + 2 + 20 + 200
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { BetweenOneAndTwo = 'B' },
                LaneToMove = 2,
                Expected = new GameBoardBuilder { LeftColumn = { FirstPosition = 'B' } },
                ExpectedScore = 20
            }
        }
    };

    public static object[][] MoveMiddleItemRightScenarios => new[]
    {
        new object[]
        {
            new TestScenario
            {
                Start =
                    new GameBoardBuilder
                    {
                        BetweenOneAndTwo = 'A', BetweenTwoAndThree = 'B', BetweenThreeAndFour = 'C', RightColumn = { FirstPosition = 'B' }
                    },
                LaneToMove = 2,
                Expected =
                    new GameBoardBuilder
                    {
                        BetweenTwoAndThree = 'A', BetweenThreeAndFour = 'B', RightColumn = { FirstPosition = 'C', SecondPosition = 'B' }
                    },
                ExpectedScore = 10 + 2 + 20 + 200
            }
        },
        new object[]
        {
            new TestScenario
            {
                Start = new GameBoardBuilder { BetweenOneAndTwo = 'A', BetweenThreeAndFour = 'C' },
                LaneToMove = 4,
                Expected = new GameBoardBuilder { BetweenOneAndTwo = 'A', RightColumn = new ColumnBuilder { FirstPosition = 'C' } },
                ExpectedScore = 200
            }
        }
    };

    [Fact]
    public void StepOne_ShouldWorkWithExample()
    {
        _sut.DetermineStepOneResult(_reader)
            .Should()
            .Be(expected: 12521L);
    }

    [Fact]
    public void StepTwo_ShouldWorkWithExample()
    {
        _sut.DetermineStepTwoResult(_reader)
            .Should()
            .Be(expected: 44169);
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
        PerformAndAdd(b => GameBoard.MoveColumnToLeft(b, column: 3));
        PerformAndAdd(b => GameBoard.MoveMiddleItemLeft(b, index: 3));
        sum.Should()
            .Be(expected: 40);

        PerformAndAdd(b => GameBoard.MoveColumnToRight(b, column: 2));
        PerformAndAdd(b => GameBoard.MoveItemInFromLeft(b, column: 3));
        sum.Should()
            .Be(40 + 400);

        PerformAndAdd(b => GameBoard.MoveColumnToRight(b, column: 2));
        PerformAndAdd(b => GameBoard.MoveItemInFromLeft(b, column: 2));
        sum.Should()
            .Be(40 + 400 + 3000 + 30);

        PerformAndAdd(b => GameBoard.MoveColumnToRight(b, column: 1));
        PerformAndAdd(b => GameBoard.MoveItemInFromLeft(b, column: 2));
        sum.Should()
            .Be(40 + 400 + 3000 + 30 + 40);

        PerformAndAdd(b => GameBoard.MoveColumnToLeft(b, column: 4));
        PerformAndAdd(b => GameBoard.MoveColumnToRight(b, column: 4));
        sum.Should()
            .Be(40 + 400 + 3000 + 30 + 40 + 2003);

        PerformAndAdd(b => GameBoard.MoveItemInFromLeft(b, column: 4));
        PerformAndAdd(b => GameBoard.MoveItemInFromLeft(b, column: 4));
        sum.Should()
            .Be(40 + 400 + 3000 + 30 + 40 + 2003 + 7000);

        PerformAndAdd(b => GameBoard.MoveItemInFromRight(b, column: 1));
        sum.Should()
            .Be(expected: 12521);

        GameBoard.IsCorrect(exampleBoard)
            .Should()
            .BeTrue();

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
        var exampleBoard = new[] { '.', '.', '.', '.', '.', '.', '.', 'B', 'D', 'D', 'A', 'C', 'C', 'B', 'D', 'B', 'B', 'A', 'C', 'D', 'A', 'C', 'A' };

        long sum = 0;
        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, column: 4));
        PerformAndAdd(b => GameBoardLarge.MoveColumnToLeft(b, column: 4));
        PerformAndAdd(b => GameBoardLarge.MoveMiddleItemLeft(b, index: 4));
        PerformAndAdd(b => GameBoardLarge.MoveMiddleItemLeft(b, index: 3));
        PerformAndAdd(b => GameBoardLarge.MoveMiddleItemLeft(b, index: 2));
        long expectedSum = 2000 + 9;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, column: 3));
        PerformAndAdd(b => GameBoardLarge.MoveMiddleItemRight(b, index: 4));
        expectedSum += 1000 + 40;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, column: 3));
        expectedSum += 30;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToLeft(b, column: 3));
        PerformAndAdd(b => GameBoardLarge.MoveMiddleItemLeft(b, index: 3));
        PerformAndAdd(b => GameBoardLarge.MoveMiddleItemLeft(b, index: 2));
        expectedSum += 9;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, column: 2));
        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, column: 3));
        expectedSum += 600;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, column: 2));
        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, column: 3));
        expectedSum += 600;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, column: 2));
        expectedSum += 40;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToLeft(b, column: 2));
        expectedSum += 5000;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveItemInFromRight(b, column: 2));
        PerformAndAdd(b => GameBoardLarge.MoveItemInFromRight(b, column: 2));
        PerformAndAdd(b => GameBoardLarge.MoveItemInFromRight(b, column: 2));
        expectedSum += 180;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToLeft(b, column: 4));
        PerformAndAdd(b => GameBoardLarge.MoveItemInFromRight(b, column: 3));
        expectedSum += 600;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, column: 4));
        expectedSum += 5;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, column: 4));
        expectedSum += 9000;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, column: 1));
        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, column: 2));
        expectedSum += 40;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, column: 1));
        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, column: 4));
        expectedSum += 11000;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveColumnToRight(b, column: 1));
        expectedSum += 4000;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, column: 1));
        expectedSum += 4;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, column: 4));
        expectedSum += 7000;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveItemInFromLeft(b, column: 1));
        expectedSum += 4;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveItemInFromRight(b, column: 1));
        expectedSum += 8;
        sum.Should()
            .Be(expectedSum);

        PerformAndAdd(b => GameBoardLarge.MoveItemInFromRight(b, column: 4));
        expectedSum += 3000;
        sum.Should()
            .Be(expectedSum);

        sum.Should()
            .Be(expected: 44169);
        GameBoardLarge.IsCorrect(exampleBoard)
            .Should()
            .BeTrue();

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

    [Theory]
    [MemberData(nameof(MoveLeftScenarios))]
    public void GameBoard_MoveLeftScenarios(TestScenario testScenario)
    {
        var start = testScenario.Start.ToArray();
        var result = GameBoard.MoveColumnToLeft(start, testScenario.LaneToMove);

        result.Should()
            .BeEquivalentTo((testScenario.Expected?.ToArray() ?? Array.Empty<char>(), testScenario.ExpectedScore));
    }

    [Theory]
    [MemberData(nameof(MoveRightScenarios))]
    public void GameBoard_MoveRightScenarios(TestScenario testScenario)
    {
        var start = testScenario.Start.ToArray();
        var result = GameBoard.MoveColumnToRight(start, testScenario.LaneToMove);

        result.Should()
            .BeEquivalentTo((testScenario.Expected?.ToArray() ?? Array.Empty<char>(), testScenario.ExpectedScore));
    }

    [Theory]
    [MemberData(nameof(MoveItemInFromLeftScenarios))]
    public void GameBoard_MoveItemInFromLeft(TestScenario testScenario)
    {
        var result = GameBoard.MoveItemInFromLeft(testScenario.Start.ToArray(), testScenario.LaneToMove);

        result.Should()
            .BeEquivalentTo((testScenario.Expected?.ToArray() ?? Array.Empty<char>(), testScenario.ExpectedScore));
    }

    [Theory]
    [MemberData(nameof(MoveItemInFromRightScenarios))]
    public void GameBoard_MoveItemInFromRight(TestScenario testScenario)
    {
        var result = GameBoard.MoveItemInFromRight(testScenario.Start.ToArray(), testScenario.LaneToMove);

        result.Should()
            .BeEquivalentTo((testScenario.Expected?.ToArray() ?? Array.Empty<char>(), testScenario.ExpectedScore));
    }

    [Theory]
    [MemberData(nameof(MoveMiddleItemLeftScenarios))]
    public void GameBoard_MoveMiddleItemLeft(TestScenario scenario)
    {
        GameBoard.MoveMiddleItemLeft(scenario.Start.ToArray(), scenario.LaneToMove)
            .Should()
            .BeEquivalentTo((scenario.Expected.ToArray(), scenario.ExpectedScore));
    }

    [Theory]
    [MemberData(nameof(MoveMiddleItemRightScenarios))]
    public void GameBoard_MoveMiddleItemRight(TestScenario scenario)
    {
        GameBoard.MoveMiddleItemRight(scenario.Start.ToArray(), scenario.LaneToMove)
            .Should()
            .BeEquivalentTo((scenario.Expected.ToArray(), scenario.ExpectedScore));
    }

    public class TestScenario
    {
        internal GameBoardBuilder Start { get; init; } = new();
        public int LaneToMove { get; init; }
        internal GameBoardBuilder? Expected { get; init; }
        public int ExpectedScore { get; init; }
    }
}
