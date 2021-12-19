using System.Numerics;
using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal class Day19 : BaseSolution
{
    public Day19()
        : base(@"Beacon Scanner", 2021, 19)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var parsed = ParseScanners(reader).ToArray();
        var visitedScanners = new HashSet<Scanner>();
        for (var i = 0; i < parsed.Length - 1; i++)
        {
            var scanner = parsed[i];
            if (visitedScanners.Contains(scanner))
            {
                continue;
            }
            for (var j = i + 1; j < parsed.Length; j++)
            {
                var otherSanner = parsed[j];
                if (visitedScanners.Contains(otherSanner))
                {
                    continue;
                }

                if (scanner.HasOverlappingVectorsWith(otherSanner, out var matrix))
                {

                }

            }
        }
        return -1;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<Scanner> ParseScanners(FileReader reader)
    {
        var regex = new Regex(@"--- scanner (\d+) ---");
        var points = new LinkedList<Vector3>();
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
                currentID = int.Parse(match.Groups[1].Value);
                points.Clear();
                continue;
            }

            var split = line.Split(',');
            points.AddLast(new Vector3(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2])));
        }

        yield return new Scanner(currentID, points.ToArray());
    }

    public class Scanner
    {
        public int Id { get; }
        public HashSet<Vector3> Vectors { get; }

        private static readonly Lazy<Matrix4x4[]> rotationMatrices;

        static Scanner()
        {
            rotationMatrices = new(() => EnumerateRotations().ToArray());
        }
        public Scanner(int id, Vector3[] vectors)
        {
            Id = id;
            Vectors = new(vectors);
        }

        public bool HasOverlappingVectorsWith(Scanner other, out Matrix4x4 matrix)
        {
            foreach (var origin in Vectors)
            {
                var translateToOrigin = Matrix4x4.CreateTranslation(-origin);
                foreach (var point in other.Vectors)
                {
                    foreach (var rotation in rotationMatrices.Value)
                    {
                        var translate = Matrix4x4.CreateTranslation(origin - Vector3.Transform(point, rotation));
                        var containsOtherCount = other.Vectors
                            .Where(vector =>
                            {
                                var transformed = Vector3.Transform(vector, translate);
                                transformed = transformed with
                                {
                                    X = MathF.Round(transformed.X),
                                    Y = MathF.Round(transformed.Y),
                                    Z = MathF.Round(transformed.Z)
                                };
                                return Vectors.Contains(transformed);
                            })
                            .Count();
                        if (containsOtherCount == 0)
                        {
                            throw new InvalidOperationException("It should always match at least one");
                        }
                        if (containsOtherCount >= 12)
                        {
                            matrix = translate;
                            return true;
                        }
                    }
                }
            }
            matrix = default;
            return false;
        }

        private static IEnumerable<Matrix4x4> EnumerateRotations()
        {
            for (var x = 0; x < 4; x++)
            {
                for (var y = 0; y < 4; y++)
                {
                    for (var z = 0; z < 4; z++)
                    {
                        var matrix = Matrix4x4.CreateFromYawPitchRoll(
                            (MathF.PI / 2) * x,
                            (MathF.PI / 2) * y,
                            (MathF.PI / 2) * z
                        );
                        yield return matrix;
                    }
                }
            }
        }

    }
}