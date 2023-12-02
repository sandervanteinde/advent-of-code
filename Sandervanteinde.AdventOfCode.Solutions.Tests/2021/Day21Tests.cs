using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions._2021;
using Sandervanteinde.AdventOfCode.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2021;

public class Day21Tests
{
    private readonly FileReader _reader;
    private readonly Day21 _sut;

    public Day21Tests()
    {
        _sut = new Day21();
        _reader = new FileReader(
            @"Player 1 starting position: 4
Player 2 starting position: 8"
        );
    }

    [Fact]
    public void StepOne_ShouldWorkWithExample()
    {
        _sut.DetermineStepOneResult(_reader)
            .Should()
            .Be(expected: 739785);
    }

    [Fact]
    public void StepTwo_ShouldWorkWithExample()
    {
        _sut.DetermineStepTwoResult(_reader)
            .Should()
            .Be(expected: 444356092776315);
    }
}
