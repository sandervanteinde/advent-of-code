using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;
using Sandervanteinde.AdventOfCode.Solutions._2021;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2021;

public class Day13Tests
{
    private readonly Day13 _sut;
    private readonly FileReader _reader;

    public Day13Tests()
    {
        _sut = new Day13();
        _reader = new FileReader(@"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5");
    }

    [Fact]
    public void Step1_ShouldWorkWithExample()
    {
        _sut.DetermineStepOneResult(_reader).Should().Be(17);
    }
}