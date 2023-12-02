using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions._2021;
using Sandervanteinde.AdventOfCode.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2021;

public class Day17Tests
{
    private readonly FileReader _reader;
    private readonly Day17 _sut;

    public Day17Tests()
    {
        _sut = new Day17();
        _reader = new FileReader(@"target area: x=20..30, y=-10..-5");
    }

    [Fact]
    public void StepOne_ShouldWorkWithExample()
    {
        _sut.DetermineStepOneResult(_reader)
            .Should()
            .Be(expected: 45);
    }

    [Fact]
    public void StepTwo_ShouldWorkWithExample()
    {
        _sut.DetermineStepTwoResult(_reader)
            .Should()
            .Be(expected: 112);
    }
}
