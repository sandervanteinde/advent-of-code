using FluentAssertions;
using Xunit;
using static Sandervanteinde.AdventOfCode2021.Solutions._2016.Day07;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Tests._2016;

public class Day07Tests
{
    [Theory]
    [InlineData("aba[bab]xyz", true)]
    [InlineData("xyx[xyx]xyx", false)]
    [InlineData("aaa[kek]eke", true)]
    [InlineData("zazbz[bzb]cdb", true)]
    public void Code_ValidatesChecksum(string ipv7, bool supportsSsl)
    {
        // arrange
        var address = new Ipv7Address(ipv7);


        address.SupportsSsl().Should().Be(supportsSsl);
    }
}
