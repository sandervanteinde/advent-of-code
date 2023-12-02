using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions._2021;
using Sandervanteinde.AdventOfCode.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2021;

public class Day12Tests
{
    [Fact]
    public void Part1_ShouldWorkWithExample()
    {
        var exampleInput = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end";
        var reader = new FileReader(exampleInput);

        var day12 = new Day12();

        var result = day12.DetermineStepOneResult(reader);

        result.Should()
            .Be(expected: 10);
    }

    [Fact]
    public void Part2_ShouldWorkWithExample()
    {
        var exampleInput = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end";
        var reader = new FileReader(exampleInput);

        var day12 = new Day12();

        var result = day12.DetermineStepTwoResult(reader);

        result.Should()
            .Be(expected: 36);
    }
}
