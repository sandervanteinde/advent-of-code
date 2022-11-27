using FluentAssertions;
using Xunit;

using static Sandervanteinde.AdventOfCode.Solutions._2016.Day08;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2016;

public class Day08Tests
{
    private readonly Screen _sut;

    public Day08Tests()
    {
        _sut = new Screen(5, 5);
    }
    [Fact]
    public void RotateRow_Should_RotateRow()
    {
        _sut.CreateRectangle(2, 2);

        _sut.RotateRow(0, 2);

        var result = _sut.Print();
        _ = result.Should().Be(@"..##.
##...
.....
.....
.....
");
    }

    [Fact]
    public void RotateColumn_Should_RotateColumn()
    {

        _sut.CreateRectangle(3, 3);

        _sut.RotateColumn(2, 3);

        var result = _sut.Print();
        _ = result.Should().Be(@"###..
##...
##...
..#..
..#..
");
    }

    [Fact]
    public void CreateRectangle_Should_CreateRectangleInTopCorner()
    {
        _sut.CreateRectangle(3, 3);

        var result = _sut.Print();
        _ = result.Should().Be(@"###..
###..
###..
.....
.....
");

    }
}
