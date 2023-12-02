using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests.Utils;

public class PointTests
{
    [Fact]
    public void BetweenInclusive_StraightXLine()
    {
        var p1 = new Point { X = 0, Y = 0 };
        var p2 = new Point { X = 0, Y = 5 };

        Point.BetweenInclusive(p1, p2)
            .Should()
            .BeEquivalentTo(
                new[]
                {
                    new Point { X = 0, Y = 0 }, new Point { X = 0, Y = 1 }, new Point { X = 0, Y = 2 }, new Point { X = 0, Y = 3 },
                    new Point { X = 0, Y = 4 }, new Point { X = 0, Y = 5 }
                }
            );
    }

    [Fact]
    public void BetweenInclusive_StraightYLine()
    {
        var p1 = new Point { X = 0, Y = 0 };
        var p2 = new Point { X = 5, Y = 0 };

        Point.BetweenInclusive(p1, p2)
            .Should()
            .BeEquivalentTo(
                new[]
                {
                    new Point { X = 0, Y = 0 }, new Point { X = 1, Y = 0 }, new Point { X = 2, Y = 0 }, new Point { X = 3, Y = 0 },
                    new Point { X = 4, Y = 0 }, new Point { X = 5, Y = 0 }
                }
            );
    }

    [Fact]
    public void DiagonalInclusive()
    {
        var line = new Line { Start = new Point { X = 0, Y = 0 }, End = new Point { X = 3, Y = -3 } };

        Point.DiagonalInclusive(line)
            .Should()
            .BeEquivalentTo(new[] { new Point { X = 0, Y = 0 }, new Point { X = 1, Y = -1 }, new Point { X = 2, Y = -2 }, new Point { X = 3, Y = -3 } });
    }
}
