using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions._2016;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2016;

public class Day9Tests
{
    [Theory]
    [InlineData("ADVENT", 6)]
    [InlineData("A(1x5)BC", 7)]
    [InlineData("(3x3)XYZ", 9)]
    [InlineData("A(2x2)BCD(2x2)EFG", 11)]
    [InlineData("(6x1)(1x3)A", 6)]
    [InlineData("X(8x2)(3x3)ABCY", 18)]
    public void StepOne(string input, int result)
    {
        var sut = new Day09();

        sut.GetStepOneResult(input).Should().Be(result.ToString());
    }

    [Theory]
    [InlineData("(3x3)XYZ", 9)]
    [InlineData("X(8x2)(3x3)ABCY", 20)]
    public void StepTwo(string input, ulong result)
    {
        var sut = new Day09();

        sut.GetDecodedLengthVersion2(input).Should().Be(result);
    }
}
