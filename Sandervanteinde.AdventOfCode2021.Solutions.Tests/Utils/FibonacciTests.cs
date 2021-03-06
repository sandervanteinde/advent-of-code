using FluentAssertions;
using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using System.Linq;
using Xunit;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Tests.Utils;

public class FibonacciTests
{
    [Fact]
    public void Fibonacci_FirstTen_ShouldBeCorrect()
    {
        Fibonacci.Enumerate()
            .Take(10)
            .Should()
            .BeEquivalentTo(new[] { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 });
    }
}
