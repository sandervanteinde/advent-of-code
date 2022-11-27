using Sandervanteinde.AdventOfCode.Solutions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal class Day20 : BaseSolution
{
    public Day20()
        : base(@"Trench Map", 2021, 20)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        return Iterate(reader, 2);
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        return Iterate(reader, 50);
    }

    private static int Iterate(FileReader reader, int iterateCount)
    {

        var (isOnDictionary, pointsThatAreOn, area) = ParseInput(reader);
        var outsideArea = false;
        for (var i = 0; i < iterateCount; i++)
        {
            var areaToScan = new Area { Left = area.Left - 1, Right = area.Right + 1, Top = area.Top - 1, Bottom = area.Bottom + 1 };
            var newArea = new Area();
            var newPoints = new HashSet<Point>();
            foreach (var point in areaToScan.IteratePointsInArea())
            {
                var binaryValue = 0;
                for (var y = -1; y <= 1; y++)
                {
                    for (var x = -1; x <= 1; x++)
                    {
                        var neighbouringPoint = new Point { X = point.X + x, Y = point.Y + y };
                        var isOn = area.IsInArea(neighbouringPoint)
                            ? pointsThatAreOn.Contains(neighbouringPoint)
                            : outsideArea;
                        binaryValue <<= 1;
                        if (isOn)
                        {
                            binaryValue |= 1;
                        }
                    }
                }
                if (isOnDictionary[binaryValue])
                {
                    newArea.ExpandToFit(point);
                    newPoints.Add(point);
                }
            }
            area = newArea;
            pointsThatAreOn = newPoints;
            outsideArea = isOnDictionary[0] ? !outsideArea : false;
        }
        return pointsThatAreOn.Count;
    }

    private static (Dictionary<int, bool> isOnLookup, HashSet<Point> image, Area area) ParseInput(FileReader reader)
    {
        var lines = reader.ReadLineByLine();
        var firstLine = lines.First();

        var isOnDictionary = firstLine
            .Select((on, index) => new { on, index })
            .ToDictionary(x => x.index, x => x.on == '#');

        var innerReader = new FileReader(string.Join(Environment.NewLine, lines.Skip(1)));
        var image = innerReader.ReadAsGrid(c => c == '#');
        var hashSet = new HashSet<Point>();
        var area = new Area();
        foreach (var item in GridUtils.EnumerateGrid(image))
        {
            if (item.Value)
            {
                hashSet.Add(new Point { X = item.X, Y = item.Y });
                area.ExpandToFit(item.X, item.Y);
            }
        }

        return (isOnDictionary, hashSet, area);
    }
}