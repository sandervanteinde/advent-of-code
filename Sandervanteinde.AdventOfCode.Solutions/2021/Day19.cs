using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day19 : BaseSolution
{
    public Day19()
        : base(@"Beacon Scanner", year: 2021, day: 19)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var parsed = ParseScanners(reader)
            .ToArray();
        var scannersToScan = new Queue<Scanner>(new[] { parsed[0] });
        var visited = new HashSet<Scanner> { parsed[0] };
        var beaconLocations = new HashSet<Point3D>(parsed[0].Vectors);

        while (scannersToScan.Count > 0)
        {
            var scanner = scannersToScan.Dequeue();

            foreach (var otherScanner in parsed)
            {
                if (visited.Contains(otherScanner))
                {
                    continue;
                }

                if (otherScanner.HasOverlappingVectorsWith(scanner, out var matrix, out var translated))
                {
                    visited.Add(otherScanner);
                    var scannerAsOrigin = new Scanner(otherScanner.Id, translated);
                    beaconLocations.UnionWith(translated);
                    scannersToScan.Enqueue(scannerAsOrigin);
                }
            }
        }

        return beaconLocations.Count;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var parsed = ParseScanners(reader)
            .ToArray();
        var scannersToScan = new Queue<Scanner>(new[] { parsed[0] });
        var visited = new HashSet<Scanner> { parsed[0] };
        var locations = new Dictionary<Scanner, Point3D> { { parsed[0], new Point3D() } };

        while (scannersToScan.Count > 0)
        {
            var scanner = scannersToScan.Dequeue();

            foreach (var otherScanner in parsed)
            {
                if (visited.Contains(otherScanner))
                {
                    continue;
                }

                if (otherScanner.HasOverlappingVectorsWith(scanner, out var matrix, out var translated))
                {
                    visited.Add(otherScanner);
                    var scannerAsOrigin = new Scanner(otherScanner.Id, translated);
                    scannersToScan.Enqueue(scannerAsOrigin);
                    locations.Add(scannerAsOrigin, matrix.Translation);
                }
            }
        }

        return locations.SelectMany(_ => locations, (test, test2) => new { From = test.Value, To = test2.Value })
            .Max(x => Math.Abs(x.From.X - x.To.X) + Math.Abs(x.From.Y - x.To.Y) + Math.Abs(x.From.Z - x.To.Z));
    }

    private static IEnumerable<Scanner> ParseScanners(FileReader reader)
    {
        var regex = new Regex(@"--- scanner (\d+) ---");
        var points = new LinkedList<Point3D>();
        var currentID = -1;

        foreach (var line in reader.ReadLineByLine())
        {
            var match = regex.Match(line);

            if (match.Success)
            {
                if (currentID != -1)
                {
                    yield return new Scanner(currentID, points.ToArray());
                }

                currentID = int.Parse(match.Groups[groupnum: 1].Value);
                points.Clear();
                continue;
            }

            var split = line.Split(separator: ',');
            points.AddLast(new Point3D(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2])));
        }

        yield return new Scanner(currentID, points.ToArray());
    }
}
