using FluentAssertions;
using Sandervanteinde.AdventOfCode2021.Solutions._2021;
using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Tests._2021;

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
