using FluentAssertions;
using Sandervanteinde.AdventOfCode2021.Solutions._2021;
using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Tests._2021;

public class Day21Tests
{
    private readonly Day21 _sut;
    private readonly FileReader _reader;

    public Day21Tests()
    {
        _sut = new Day21();
        _reader = new FileReader(@"Player 1 starting position: 4
Player 2 starting position: 8");
    }

    [Fact]
    public void StepOne_ShouldWorkWithExample()
    {
        _sut.DetermineStepOneResult(_reader)
            .Should().Be(739785);
    }

    [Fact]
    public void StepTwo_ShouldWorkWithExample()
    {
        _sut.DetermineStepTwoResult(_reader)
            .Should().Be(444356092776315);
    }
}