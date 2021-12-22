using System.Diagnostics.CodeAnalysis;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Utils;

internal readonly record struct Area3D(int Left, int Right, int Top, int Bottom, int Front, int Back)
{
    public bool IsValid => Left <= Right && Top <= Bottom && Front <= Back;

    public Area3D(Point3D min, Point3D max)
        : this(min.X, max.X, min.Y, max.Y, min.Z, max.Z)
    {

    }

    public IEnumerable<Point3D> IterateInclusive()
    {
        for (var z = Front; z <= Back; z++)
        {
            for (var y = Top; y <= Bottom; y++)
            {
                for (var x = Left; x <= Right; x++)
                {
                    yield return new Point3D(x, y, z);
                }
            }
        }
    }

    public Area3D[] SplitX(int x)
    {
        if (x <= Left || x > Right)
        {
            return new[] { this };
        }
        return new[]
        {
            new Area3D(Left, x - 1, Top, Bottom, Front, Back),
            new Area3D(x, Right, Top, Bottom, Front, Back)
        };
    }

    public Area3D[] SplitY(int y)
    {
        if (y <= Top || y > Bottom)
        {
            return new[] { this };
        }

        return new[]
        {
            new Area3D(Left, Right, Top, y -1, Front, Back),
            new Area3D(Left, Right, y, Bottom, Front, Back)
        };
    }

    public Area3D[] SplitZ(int z)
    {
        if (z <= Front || z > Back)
        {
            return new[] { this };
        }

        return new[]
        {
            new Area3D(Left, Right, Top, Bottom, Front, z - 1),
            new Area3D(Left, Right, Top, Bottom, z, Back)
        };
    }


    public bool HasOverlapWith(in Area3D area, [NotNullWhen(true)] out Area3D overlappingArea)
    {
        overlappingArea = new Area3D(
            Left: Math.Max(Left, area.Left),
            Right: Math.Min(Right, area.Right),
            Top: Math.Max(Top, area.Top),
            Bottom: Math.Min(Bottom, area.Bottom),
            Front: Math.Max(Front, area.Front),
            Back: Math.Min(Back, area.Back)
        );
        return overlappingArea.IsValid;
    }

    internal Area3D[] SplitToFit(in Area3D innerArea)
    {
        var areas = new List<Area3D>(27) { this };

        void Add(Func<Area3D, IEnumerable<Area3D>> splitFn)
        {
            var newAreas = new List<Area3D>(27);
            newAreas.AddRange(areas.SelectMany(splitFn));
            areas = newAreas;
        }

        if (Left != innerArea.Left)
        {
            var left = innerArea.Left;
            Add(area => area.SplitX(left));
        }

        if (Right != innerArea.Right)
        {
            var right = innerArea.Right;
            Add(area => area.SplitX(right + 1));
        }

        if (Top != innerArea.Top)
        {
            var top = innerArea.Top;
            Add(area => area.SplitY(top));
        }
        if (Bottom != innerArea.Bottom)
        {
            var bottom = innerArea.Bottom;
            Add(area => area.SplitY(bottom + 1));
        }
        if (Front != innerArea.Front)
        {
            var front = innerArea.Front;
            Add(area => area.SplitZ(front));
        }
        if (Back != innerArea.Back)
        {
            var back = innerArea.Back;
            Add(area => area.SplitZ(back + 1));
        }

        return areas.ToArray();
    }

    public long VolumeInclusive()
    {
        return (long)(Right - Left + 1) * (Bottom - Top + 1) * (Back - Front + 1);
    }

    public bool IsPointInArea(Point3D point)
    {
        return point.X >= Left && point.X <= Right
            && point.Y >= Top && point.Y <= Bottom
            && point.Z >= Front && point.Z <= Back;
    }
}
