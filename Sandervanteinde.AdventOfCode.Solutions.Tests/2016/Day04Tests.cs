using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions._2016;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2016;

public class Day04Tests
{
    [Theory]
    [InlineData("aaaaa-bbb-z-y-x", 123, "abxyz", true)]
    [InlineData("a-b-c-d-e-f-g-h", 987, "abcde", true)]
    [InlineData("not-a-real-room", 404, "oarel", true)]
    [InlineData("totally-real-room", 200, "decoy", false)]
    public void Code_ValidatesChecksum(string name, int sectorId, string checkSum, bool isValid)
    {
        // arrange
        var code = new Day04.Code
        {
            SectorId = sectorId,
            Checksum = checkSum,
            Name = name
        };

        // act
        var result = code.IsValidChecksum();

        // assert
        result.Should().Be(isValid);
    }

    [Theory]
    [InlineData("a", 1, "b")]
    [InlineData("abc", 1, "bcd")]
    [InlineData("abc", 26, "abc")]
    [InlineData("abc", 27, "bcd")]
    public void Decipher_ShouldCorrectlyDecipher(string name, int sectorId, string decipheredText)
    {
        // arrange
        var code = new Day04.Code
        {
            Checksum = "",
            Name = name,
            SectorId = sectorId
        };

        // act
        var result = code.Decipher();

        // assert
        result.Should().Be(decipheredText);
    }
}
