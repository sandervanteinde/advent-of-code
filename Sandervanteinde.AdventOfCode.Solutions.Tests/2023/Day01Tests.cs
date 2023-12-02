using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions._2023;
using Sandervanteinde.AdventOfCode.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2023;

public class Day01Tests
{
    private readonly Day01 _sut;

    public Day01Tests()
    {
        _sut = new Day01();
    }

    [Theory]
    [InlineData("two1nine", 29)]
    [InlineData("eightwothree", 83)]
    [InlineData("abcone2threexyz", 13)]
    [InlineData("xtwone3four", 24)]
    [InlineData("4nineeightseven2", 42)]
    [InlineData("zoneight234", 14)]
    [InlineData("7pqrstsixteen", 76)]
    [InlineData("eightone7threenl7mtxbmkpkzqzljrdk", 87)]
    public void PartTwoTests(string input, int expected)
    {
        var reader = new FileReader(input);

        var result = _sut.DetermineStepTwoResult(reader);

        result.Should()
            .Be(expected);
    }
}
