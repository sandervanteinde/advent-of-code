namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day19
{
    public class Scanner
    {
        public int Id { get; }
        public HashSet<Point3D> Vectors { get; }

        public Scanner(int id, Point3D[] vectors)
        {
            Id = id;
            Vectors = new(vectors);
        }

        public bool HasOverlappingVectorsWith(Scanner other, out Matrix? matrix, out Point3D[] translatedVectors)
        {
            for (var rotation = 0; rotation < 24; rotation++)
            {
                var rotated = Vectors.Select(v => v.Rotate(rotation)).ToArray();
                foreach (var vector in other.Vectors)
                {
                    foreach (var rotatedVector in rotated)
                    {
                        var offset = vector - rotatedVector;
                        var translated = rotated.Select(s => s + offset).ToArray();
                        var count = other.Vectors.Intersect(translated).Count();
                        if (count >= 12)
                        {
                            matrix = new Matrix(offset, rotation);
                            translatedVectors = translated;
                            return true;
                        }
                    }
                }
            }
            matrix = null;
            translatedVectors = Array.Empty<Point3D>();
            return false;
        }
    }
}