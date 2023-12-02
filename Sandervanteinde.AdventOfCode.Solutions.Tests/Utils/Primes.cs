using System.Linq;
using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests.Utils;

public class PrimesTests
{
    [Theory]
    [InlineData(13195, new[] { 5, 7, 13, 29 })]
    public void FactorsOf(int value, int[] expectedValues)
    {
        Primes.FactorsOf(value)
            .Distinct()
            .Should()
            .BeEquivalentTo(expectedValues);
    }

    [Theory]
    [InlineData(10, new[] { 2, 3, 5, 7 })]
    [InlineData(121, new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113 })]
    public void Enumerate(int max, int[] expectedValues)
    {
        Primes.Enumerate(max)
            .Should()
            .BeEquivalentTo(expectedValues);
    }
}
