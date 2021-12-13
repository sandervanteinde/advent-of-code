using FluentAssertions;
using Sandervanteinde.AdventOfCode2021.Solutions._2016;
using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Tests._2016;

public class Day02Tests
{
    private readonly FileReader _reader;
    private readonly Day02 _sut;

    public Day02Tests()
    {
        _reader = new FileReader(@"ULL
RRDDD
LURDL
UUUUD");
        _sut = new Day02();

    }
    [Fact]
    public void StepOne_ShouldWorkWithExample()
    {
        _sut.DetermineStepOneResult(_reader).Should().Be("1985");
    }

    [Fact]
    public void StepTwo_ShouldWorkWithExample()
    {
        _sut.DetermineStepTwoResult(_reader).Should().Be("5DB3");
    }
}
