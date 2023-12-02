using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions._2016;
using Sandervanteinde.AdventOfCode.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2016;

public class Day01Tests
{
    [Fact]
    public void StepTwo_ShouldWorkWithExample()
    {
        var reader = new FileReader(@"R8, R4, R4, R8");
        var sut = new Day01();

        var result = sut.DetermineStepTwoResult(reader);

        result.Should()
            .Be(expected: 4);
    }
}
