using FluentAssertions;
using Sandervanteinde.AdventOfCode2021.Solutions._2021;
using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using System;
using Xunit;
using static Sandervanteinde.AdventOfCode2021.Solutions._2021.Day18;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Tests._2021;

public class Day18Tests
{
    private readonly Day18 _sut;
    private readonly FileReader _reader;

    public Day18Tests()
    {
        _sut = new Day18();
        _reader = new FileReader(@"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]");
    }

    [Fact]
    public void StepOne_ShouldWorkWithExample()
    {
        _sut.DetermineStepOneResult(_reader)
            .Should().Be(4140);
    }


    [Fact]
    public void StepTwo_ShouldWorkWithExample()
    {
        _sut.DetermineStepTwoResult(_reader)
            .Should().Be(3993);
    }


    [Theory]
    [InlineData("[[1,2],[[3,4],5]]", 143)]
    [InlineData("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", 1384)]
    [InlineData("[[[[1,1],[2,2]],[3,3]],[4,4]]", 445)]
    [InlineData("[[[[3,0],[5,3]],[4,4]],[5,5]]", 791)]
    [InlineData("[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137)]
    [InlineData("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488)]
    public void Snailfish_Magnitude_ShouldWork(string input, int expectedResult)
    {
        var snailfish = SnailfishBase.Parse(input);
        snailfish.Magnitude().Should().Be(expectedResult);
    }

    [Fact]
    public void SnailfishBaseParse_ShouldParseSingleValue()
    {
        var value = "1";

        var parsed = SnailfishBase.Parse(value);

        parsed.Should().BeOfType<Snailfish>().Which.Value.Should().Be(1);
    }

    [Fact]
    public void SnailfishBaseParse_ShouldParsePairs()
    {
        var value = "[1,2]";

        var parsed = SnailfishBase.Parse(value);

        parsed.Should().BeEquivalentTo(new
        {
            Left = new { Value = 1 },
            Right = new { Value = 2 }
        });
        TakeAs<SnailfishPair>(parsed).IsHierarchyIntact().Should().BeTrue();
    }

    [Fact]
    public void SnailfishBaseParse_ShouldParseComplexObjects()
    {
        var value = "[[2,[7,8]],[3,4]]";

        var parsed = SnailfishBase.Parse(value);

        parsed.Should().BeEquivalentTo(new
        {
            Left = new
            {
                Left = new
                {
                    Value = 2
                },
                Right = new
                {
                    Left = new { Value = 7 },
                    Right = new { Value = 8 }
                }
            },
            Right = new
            {
                Left = new { Value = 3 },
                Right = new { Value = 4 }
            }
        });
        TakeAs<SnailfishPair>(parsed).IsHierarchyIntact().Should().BeTrue();
    }

    [Fact]
    public void SnailfishPair_Explode_ShouldWork()
    {
        var pair = SnailfishBase.Parse("[[[[[1,2],3],1],2],6]");

        var explode = pair.AttemptExplode();
        explode.Should().BeTrue();
        pair.ToString().Should().Be("[[[[0,5],1],2],6]");
        TakeAs<SnailfishPair>(pair).IsHierarchyIntact().Should().BeTrue();
    }

    [Fact]
    public void Snailfish_Split_ShouldWork()
    {
        var pair = new SnailfishPair
        {
            Left = new Snailfish(11),
            Right = new Snailfish(2)
        };

        var splitSucceeded = pair.AttemptSplit();

        splitSucceeded.Should().BeTrue();
        pair.Should().BeEquivalentTo(new
        {
            Left = new
            {
                Left = new { Value = 5 },
                Right = new { Value = 6 }
            },
            Right = new { Value = 2 }
        });
        TakeAs<SnailfishPair>(pair).IsHierarchyIntact().Should().BeTrue();
    }

    [Fact]
    public void SnailfishPair_FindClosestLeft_ShouldWork()
    {
        var sut = SnailfishBase.Parse("[[[2,3],[7,8]],[2,3]]");
        var pair = TakeAs<SnailfishPair>(sut, Direction.Right);
        pair.FindClosestLeft()!.Value.Should().Be(8);
    }

    [Fact]
    public void SnailfishPair_FindClosestRight_ShouldWork()
    {
        var sut = SnailfishBase.Parse("[[[2,3],[7,8]],[2,3]]");
        var pair = TakeAs<SnailfishPair>(sut, Direction.Left, Direction.Right);
        pair.FindClosestRight()!.Value.Should().Be(2);
    }

    [Fact]
    public void SnailfishPair_FindClosestRight_ShouldReturnNull()
    {
        var sut = SnailfishBase.Parse("[[[2,3],[7,8]],[2,3]]");
        var pair = TakeAs<SnailfishPair>(sut, Direction.Right);
        pair.FindClosestRight().Should().BeNull();
    }

    private enum Direction { Left, Right };
    private TSnailfish TakeAs<TSnailfish>(SnailfishBase snailfish, params Direction[] directions)
        where TSnailfish : SnailfishBase
    {
        foreach (var direction in directions)
        {
            var pair = (SnailfishPair)snailfish;
            snailfish = direction switch
            {
                Direction.Left => pair.Left,
                Direction.Right => pair.Right,
                _ => throw new InvalidOperationException()
            };
        }
        return (TSnailfish)snailfish;
    }
}