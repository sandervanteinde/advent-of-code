using FluentAssertions;
using Sandervanteinde.AdventOfCode2021.Solutions._2021;
using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Tests._2021;

public class Day17Tests
{
    private readonly Day17 _sut;
    private readonly FileReader _reader;

    public Day17Tests()
    {
        _sut = new Day17();
        _reader = new FileReader(@"target area: x=20..30, y=-10..-5");
    }

    [Fact]
    public void StepOne_ShouldWorkWithExample()
    {
        _sut.DetermineStepOneResult(_reader)
            .Should().Be(45);
    }

    [Fact]
    public void StepTwo_ShouldWorkWithExample()
    {
        _sut.DetermineStepTwoResult(_reader)
            .Should().Be(112);
    }
}