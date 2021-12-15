using FluentAssertions;
using Sandervanteinde.AdventOfCode2021.Solutions._2021;
using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Tests._2021;

public class Day15Tests
{
    private readonly FileReader _reader;
    private readonly Day15 _sut;

    public Day15Tests()
    {
        _reader = new FileReader(@"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581");

        _sut = new Day15();
    }

    [Fact]
    public void StepOne_ShouldWorkWithExample()
    {
        _sut.DetermineStepOneResult(_reader)
            .Should().Be(40);
    }

    [Fact]
    public void StepTwo_ShouldWorkWithExample()
    {
        _sut.DetermineStepTwoResult(_reader)
            .Should().Be(315);
    }
}
