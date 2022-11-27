using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions._2021;
using Sandervanteinde.AdventOfCode.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2021;

public class Day25Tests
{
    [Fact]
    public void StepOne_ShouldWorkWithExample()
    {
        var sut = new Day25();
        var reader = new FileReader(@"v...>>.vv>
.vv>>.vv..
>>.>v>...v
>>v>>.>.v.
v>v.vv.v..
>.>>..v...
.vv..>.>v.
v.v..>>v.v
....v..v.>");

        sut.DetermineStepOneResult(reader)
            .Should().Be(58);

    }
}
