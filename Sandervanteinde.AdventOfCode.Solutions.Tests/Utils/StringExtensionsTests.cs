using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions.Extensions;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests.Utils;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("9009", true)]
    [InlineData("90109", true)]
    [InlineData("1337", false)]
    public void IsPalindrome(string input, bool expected)
    {
        input.IsPalindrome().Should().Be(expected);
    }
}
