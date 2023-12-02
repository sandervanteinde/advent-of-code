using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions._2015;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2015;

public class Day25Tests
{
    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(2, 1, 2)]
    [InlineData(1, 2, 3)]
    [InlineData(3, 1, 4)]
    [InlineData(2, 2, 5)]
    [InlineData(1, 3, 6)]
    [InlineData(1, 6, 21)]
    [InlineData(6, 1, 16)]
    public void Test(long row, long column, long result)
    {
        Day25.DetermineId(row, column)
            .Should()
            .Be(result);
    }

    [Fact]
    public void Test2()
    {
        Day25.DetermineNextValue(currentValue: 20151125)
            .Should()
            .Be(expected: 31916031);
    }
}
