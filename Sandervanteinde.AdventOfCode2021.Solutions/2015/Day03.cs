using Sandervanteinde.AdventOfCode2021.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal class Day03 : BaseSolution
{
    private struct Point
    {
        public int X { get; init; }
        public int Y { get; init; }
    }
    public Day03()
        : base("Perfectly Spherical Houses in a Vacuum", 2015, 3)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var location = new Point();
        var points = new Dictionary<Point, int>()
        {
            { location, 1 }
        };

        foreach(var c in reader.ReadCharByChar())
        {
            location = c switch
            {
                '>' => new Point { X = location.X + 1, Y = location.Y },
                '<' => new Point { X = location.X - 1, Y = location.Y },
                'v' => new Point { X = location.X, Y = location.Y + 1 },
                '^' => new Point { X = location.X, Y = location.Y - 1 },
                _ => throw new NotSupportedException($"The char {c} is not supported")
            };
            if(!points.TryGetValue(location, out var presents))
            {
                points[location] = 1;
            }
            else
            {
                points[location] = presents + 1;
            }
        }

        return points
            .Where(kvp => kvp.Value >= 1)
            .Count();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var isSanta = true;
        var santa = new Point();
        var roboSanta = new Point();
        var points = new Dictionary<Point, int>()
        {
            { santa, 2 }
        };

        foreach (var c in reader.ReadCharByChar())
        {
            var location = isSanta ? santa : roboSanta;
            location = c switch
            {
                '>' => new Point { X = location.X + 1, Y = location.Y },
                '<' => new Point { X = location.X - 1, Y = location.Y },
                'v' => new Point { X = location.X, Y = location.Y + 1 },
                '^' => new Point { X = location.X, Y = location.Y - 1 },
                _ => throw new NotSupportedException($"The char {c} is not supported")
            };
            if (!points.TryGetValue(location, out var presents))
            {
                points[location] = 1;
            }
            else
            {
                points[location] = presents + 1;
            }
            if(isSanta)
            {
                santa = location;
            } 
            else
            {
                roboSanta = location;
            }
            isSanta = !isSanta;
        }

        return points
            .Where(kvp => kvp.Value >= 1)
            .Count();
    }
}
