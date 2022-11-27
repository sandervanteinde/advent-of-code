using System.Numerics;

namespace Sandervanteinde.AdventOfCode.Solutions.Extensions;

internal static class Vector3Extensions
{
    public static Vector3 Rounded(this Vector3 vector)
    {
        return vector with
        {
            X = MathF.Round(vector.X),
            Y = MathF.Round(vector.Y),
            Z = MathF.Round(vector.Z)
        };
    }
}
