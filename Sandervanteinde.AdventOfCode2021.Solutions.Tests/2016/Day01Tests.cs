using FluentAssertions;
using Sandervanteinde.AdventOfCode2021.Solutions._2016;
using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Tests._2016;

public class Day01Tests
{
    [Fact]
    public void StepTwo_ShouldWorkWithExample()
    {
        var reader = new FileReader(@"R8, R4, R4, R8");
        var sut = new Day01();

        var result = sut.DetermineStepTwoResult(reader);

        result.Should().Be(4);
    }
}
