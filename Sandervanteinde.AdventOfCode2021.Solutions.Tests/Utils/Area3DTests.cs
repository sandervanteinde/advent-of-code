using FluentAssertions;
using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using System.Linq;
using Xunit;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Tests.Utils;

public class Area3DTests
{

    [Fact]
    public void OverlappingArea_Works()
    {
        var areaOne = new Area3D(0, 10, 0, 10, 0, 10);
        var areaTwo = new Area3D(5, 15, 5, 15, 5, 15);

        var isOverlap = areaOne.HasOverlapWith(areaTwo, out var overlap);

        isOverlap.Should().BeTrue();
        overlap.Should().Be(new Area3D(5, 10, 5, 10, 5, 10));
    }

    [Fact]
    public void OverlappingArea_NoOverlap_Works()
    {
        var areaOne = new Area3D(0, 10, 0, 10, 0, 10);
        var areaTwo = new Area3D(20, 25, 20, 25, 20, 25);

        var isOverlap = areaOne.HasOverlapWith(areaTwo, out _);

        isOverlap.Should().BeFalse();
    }

    [Fact]
    public void SplitX_SplitsOverXAxis()
    {
        var area = new Area3D(20, 30, 40, 50, 60, 70);
        var splitAreas = area.SplitX(25);

        splitAreas.Should().BeEquivalentTo(new[]
        {
            new Area3D(20, 24, 40, 50, 60, 70),
            new Area3D(25, 30, 40, 50, 60, 70)
        });
    }

    [Fact]
    public void SplitY_SplitsOverYAxis()
    {
        var area = new Area3D(20, 30, 40, 50, 60, 70);
        var splitAreas = area.SplitY(45);

        splitAreas.Should().BeEquivalentTo(new[]
        {
            new Area3D(20, 30, 40, 44, 60, 70),
            new Area3D(20, 30, 45, 50, 60, 70)
        });
    }

    [Fact]
    public void SplitZ_SplitsOverZAxis()
    {
        var area = new Area3D(20, 30, 40, 50, 60, 70);
        var splitAreas = area.SplitZ(65);

        splitAreas.Should().BeEquivalentTo(new[]
        {
            new Area3D(20, 30, 40, 50, 60, 64),
            new Area3D(20, 30, 40, 50, 65, 70)
        });
    }

    [Fact]
    public void SplitToFit_UpperCorners()
    {
        var area = new Area3D(0, 10, 0, 10, 0, 10);
        var innerArea = new Area3D(5, 10, 5, 10, 5, 10);

        var areas = area.SplitToFit(innerArea);

        areas.Should().BeEquivalentTo(new[]
        {
            new Area3D(5, 10, 5, 10, 5, 10),
            new Area3D(5, 10, 5, 10, 0, 4),
            new Area3D(5, 10, 0, 4, 5, 10),
            new Area3D(5, 10, 0, 4, 0, 4),
            new Area3D(0, 4, 5, 10, 5, 10),
            new Area3D(0, 4, 5, 10, 0, 4),
            new Area3D(0, 4, 0, 4, 5, 10),
            new Area3D(0, 4, 0, 4, 0, 4),
        });

        areas.Sum(s => s.VolumeInclusive())
            .Should().Be(area.VolumeInclusive());
    }

    [Fact]
    public void SplitToFit_LowerCorners()
    {
        var area = new Area3D(0, 10, 0, 10, 0, 10);
        var innerArea = new Area3D(0, 5, 0, 5, 0, 5);

        var areas = area.SplitToFit(in innerArea);


        areas.Sum(s => s.VolumeInclusive())
            .Should().Be(area.VolumeInclusive());

        areas.Should().BeEquivalentTo(new[]
        {
            new Area3D(6, 10, 6, 10, 6, 10),
            new Area3D(6, 10, 6, 10, 0, 5),
            new Area3D(6, 10, 0, 5, 6, 10),
            new Area3D(6, 10, 0, 5, 0, 5),
            new Area3D(0, 5, 6, 10, 6, 10),
            new Area3D(0, 5, 6, 10, 0, 5),
            new Area3D(0, 5, 0, 5, 6, 10),
            new Area3D(0, 5, 0, 5, 0, 5),
        });
    }

    [Fact]
    public void SplitToFit_FailedSituation()
    {
        var area = new Area3D(0, 6, 0, 29, 0, 12);
        var innerArea = new Area3D(0, 6, 0, 20, 0, 11);

        var areas = area.SplitToFit(innerArea);


        areas.Sum(s => s.VolumeInclusive())
            .Should().Be(area.VolumeInclusive());
        areas.Should().Contain(innerArea);
    }
}
