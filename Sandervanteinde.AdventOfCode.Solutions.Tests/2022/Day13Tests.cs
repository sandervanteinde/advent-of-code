using System.Text.Json;
using System.Text.Json.Nodes;
using FluentAssertions;
using Xunit;
using static Sandervanteinde.AdventOfCode.Solutions._2022.Day13;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2022;
public class Day13Tests
{
    private Comparer _sut;

    public Day13Tests()
    {
        _sut = new Comparer();
    }

    [Theory]
    [InlineData("[]", "[]", 0)]
    [InlineData("[[1],4]", "[1,1,3,1,1]", 1)]
    public void Comparer(string left, string right, int expected)
    {
        var leftArray = JsonSerializer.Deserialize<JsonArray>(left);
        var rightArray = JsonSerializer.Deserialize<JsonArray>(right);

        var result = _sut.Compare(leftArray, rightArray);

        result.Should().Be(expected);
    }
}
