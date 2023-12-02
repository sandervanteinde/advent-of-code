namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day19
{
    public record Matrix(Point3D Translation, int Rotation)
    {
        public static Matrix operator +(Matrix left, Matrix right)
        {
            return new Matrix(left.Translation + right.Translation, (left.Rotation + right.Rotation) % 24);
        }
    }
}
