using FluentAssertions;
using Xunit;
using static Sandervanteinde.AdventOfCode.Solutions._2016.Day08;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2016;

public class Day08Tests
{
    private readonly Screen _sut;

    public Day08Tests()
    {
        _sut = new Screen(width: 5, height: 5);
    }

    [Fact]
    public void RotateRow_Should_RotateRow()
    {
        _sut.CreateRectangle(width: 2, height: 2);

        _sut.RotateRow(row: 0, amount: 2);

        var result = _sut.Print();
        _ = result.Should()
            .Be(
                @"..##.
##...
.....
.....
.....
"
            );
    }

    [Fact]
    public void RotateColumn_Should_RotateColumn()
    {
        _sut.CreateRectangle(width: 3, height: 3);

        _sut.RotateColumn(column: 2, amount: 3);

        var result = _sut.Print();
        _ = result.Should()
            .Be(
                @"###..
##...
##...
..#..
..#..
"
            );
    }

    [Fact]
    public void CreateRectangle_Should_CreateRectangleInTopCorner()
    {
        _sut.CreateRectangle(width: 3, height: 3);

        var result = _sut.Print();
        _ = result.Should()
            .Be(
                @"###..
###..
###..
.....
.....
"
            );
    }
}
