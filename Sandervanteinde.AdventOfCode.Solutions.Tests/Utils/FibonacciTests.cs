using System.Linq;
using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests.Utils;

public class FibonacciTests
{
    [Fact]
    public void Fibonacci_FirstTen_ShouldBeCorrect()
    {
        Fibonacci.Enumerate()
            .Take(count: 10)
            .Should()
            .BeEquivalentTo(new[] { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 });
    }
}
