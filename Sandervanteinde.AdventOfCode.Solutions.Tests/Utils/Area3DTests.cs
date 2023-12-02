using System.Linq;
using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests.Utils;

public class Area3DTests
{
    [Fact]
    public void OverlappingArea_Works()
    {
        var areaOne = new Area3D(Left: 0, Right: 10, Top: 0, Bottom: 10, Front: 0, Back: 10);
        var areaTwo = new Area3D(Left: 5, Right: 15, Top: 5, Bottom: 15, Front: 5, Back: 15);

        var isOverlap = areaOne.HasOverlapWith(areaTwo, out var overlap);

        isOverlap.Should()
            .BeTrue();
        overlap.Should()
            .Be(new Area3D(Left: 5, Right: 10, Top: 5, Bottom: 10, Front: 5, Back: 10));
    }

    [Fact]
    public void OverlappingArea_NoOverlap_Works()
    {
        var areaOne = new Area3D(Left: 0, Right: 10, Top: 0, Bottom: 10, Front: 0, Back: 10);
        var areaTwo = new Area3D(Left: 20, Right: 25, Top: 20, Bottom: 25, Front: 20, Back: 25);

        var isOverlap = areaOne.HasOverlapWith(areaTwo, out _);

        isOverlap.Should()
            .BeFalse();
    }

    [Fact]
    public void SplitX_SplitsOverXAxis()
    {
        var area = new Area3D(Left: 20, Right: 30, Top: 40, Bottom: 50, Front: 60, Back: 70);
        var splitAreas = area.SplitX(x: 25);

        splitAreas.Should()
            .BeEquivalentTo(
                new[]
                {
                    new Area3D(Left: 20, Right: 24, Top: 40, Bottom: 50, Front: 60, Back: 70),
                    new Area3D(Left: 25, Right: 30, Top: 40, Bottom: 50, Front: 60, Back: 70)
                }
            );
    }

    [Fact]
    public void SplitY_SplitsOverYAxis()
    {
        var area = new Area3D(Left: 20, Right: 30, Top: 40, Bottom: 50, Front: 60, Back: 70);
        var splitAreas = area.SplitY(y: 45);

        splitAreas.Should()
            .BeEquivalentTo(
                new[]
                {
                    new Area3D(Left: 20, Right: 30, Top: 40, Bottom: 44, Front: 60, Back: 70),
                    new Area3D(Left: 20, Right: 30, Top: 45, Bottom: 50, Front: 60, Back: 70)
                }
            );
    }

    [Fact]
    public void SplitZ_SplitsOverZAxis()
    {
        var area = new Area3D(Left: 20, Right: 30, Top: 40, Bottom: 50, Front: 60, Back: 70);
        var splitAreas = area.SplitZ(z: 65);

        splitAreas.Should()
            .BeEquivalentTo(
                new[]
                {
                    new Area3D(Left: 20, Right: 30, Top: 40, Bottom: 50, Front: 60, Back: 64),
                    new Area3D(Left: 20, Right: 30, Top: 40, Bottom: 50, Front: 65, Back: 70)
                }
            );
    }

    [Fact]
    public void SplitToFit_UpperCorners()
    {
        var area = new Area3D(Left: 0, Right: 10, Top: 0, Bottom: 10, Front: 0, Back: 10);
        var innerArea = new Area3D(Left: 5, Right: 10, Top: 5, Bottom: 10, Front: 5, Back: 10);

        var areas = area.SplitToFit(innerArea);

        areas.Should()
            .BeEquivalentTo(
                new[]
                {
                    new Area3D(Left: 5, Right: 10, Top: 5, Bottom: 10, Front: 5, Back: 10),
                    new Area3D(Left: 5, Right: 10, Top: 5, Bottom: 10, Front: 0, Back: 4),
                    new Area3D(Left: 5, Right: 10, Top: 0, Bottom: 4, Front: 5, Back: 10),
                    new Area3D(Left: 5, Right: 10, Top: 0, Bottom: 4, Front: 0, Back: 4),
                    new Area3D(Left: 0, Right: 4, Top: 5, Bottom: 10, Front: 5, Back: 10),
                    new Area3D(Left: 0, Right: 4, Top: 5, Bottom: 10, Front: 0, Back: 4),
                    new Area3D(Left: 0, Right: 4, Top: 0, Bottom: 4, Front: 5, Back: 10),
                    new Area3D(Left: 0, Right: 4, Top: 0, Bottom: 4, Front: 0, Back: 4)
                }
            );

        areas.Sum(s => s.VolumeInclusive())
            .Should()
            .Be(area.VolumeInclusive());
    }

    [Fact]
    public void SplitToFit_LowerCorners()
    {
        var area = new Area3D(Left: 0, Right: 10, Top: 0, Bottom: 10, Front: 0, Back: 10);
        var innerArea = new Area3D(Left: 0, Right: 5, Top: 0, Bottom: 5, Front: 0, Back: 5);

        var areas = area.SplitToFit(in innerArea);

        areas.Sum(s => s.VolumeInclusive())
            .Should()
            .Be(area.VolumeInclusive());

        areas.Should()
            .BeEquivalentTo(
                new[]
                {
                    new Area3D(Left: 6, Right: 10, Top: 6, Bottom: 10, Front: 6, Back: 10),
                    new Area3D(Left: 6, Right: 10, Top: 6, Bottom: 10, Front: 0, Back: 5),
                    new Area3D(Left: 6, Right: 10, Top: 0, Bottom: 5, Front: 6, Back: 10),
                    new Area3D(Left: 6, Right: 10, Top: 0, Bottom: 5, Front: 0, Back: 5),
                    new Area3D(Left: 0, Right: 5, Top: 6, Bottom: 10, Front: 6, Back: 10),
                    new Area3D(Left: 0, Right: 5, Top: 6, Bottom: 10, Front: 0, Back: 5),
                    new Area3D(Left: 0, Right: 5, Top: 0, Bottom: 5, Front: 6, Back: 10),
                    new Area3D(Left: 0, Right: 5, Top: 0, Bottom: 5, Front: 0, Back: 5)
                }
            );
    }

    [Fact]
    public void SplitToFit_FailedSituation()
    {
        var area = new Area3D(Left: 0, Right: 6, Top: 0, Bottom: 29, Front: 0, Back: 12);
        var innerArea = new Area3D(Left: 0, Right: 6, Top: 0, Bottom: 20, Front: 0, Back: 11);

        var areas = area.SplitToFit(innerArea);

        areas.Sum(s => s.VolumeInclusive())
            .Should()
            .Be(area.VolumeInclusive());
        areas.Should()
            .Contain(innerArea);
    }
}
