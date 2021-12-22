namespace Sandervanteinde.AdventOfCode2021.Solutions.Utils;

public readonly record struct Point3D(int X, int Y, int Z)
{
    public static Point3D operator -(Point3D left, Point3D right)
    {
        return new Point3D(
            left.X - right.X,
            left.Y - right.Y,
            left.Z - right.Z
        );
    }

    public static Point3D operator +(Point3D left, Point3D right)
    {
        return new Point3D(
         left.X + right.X,
         left.Y + right.Y,
         left.Z + right.Z
        );
    }

    public Point3D Rotate(int direction)
    {
        var point = this;
        return direction switch
        {
            0 => point,
            1 => new Point3D(point.Y, point.Z, point.X),
            2 => new Point3D(-point.Y, point.X, point.Z),
            3 => new Point3D(-point.X, -point.Y, point.Z),
            4 => new Point3D(point.Y, -point.X, point.Z),
            5 => new Point3D(point.Z, point.Y, -point.X),
            6 => new Point3D(point.Z, point.X, point.Y),
            7 => new Point3D(point.Z, -point.Y, point.X),
            8 => new Point3D(point.Z, -point.X, -point.Y),
            9 => new Point3D(-point.X, point.Y, -point.Z),
            10 => new Point3D(point.Y, point.X, -point.Z),
            11 => new Point3D(point.X, -point.Y, -point.Z),
            12 => new Point3D(-point.Y, -point.X, -point.Z),
            13 => new Point3D(-point.Z, point.Y, point.X),
            14 => new Point3D(-point.Z, point.X, -point.Y),
            15 => new Point3D(-point.Z, -point.Y, -point.X),
            16 => new Point3D(-point.Z, -point.X, point.Y),
            17 => new Point3D(point.X, -point.Z, point.Y),
            18 => new Point3D(-point.Y, -point.Z, point.X),
            19 => new Point3D(-point.X, -point.Z, -point.Y),
            20 => new Point3D(point.Y, -point.Z, -point.X),
            21 => new Point3D(point.X, point.Z, -point.Y),
            22 => new Point3D(-point.Y, point.Z, -point.X),
            23 => new Point3D(-point.X, point.Z, point.Y),
            _ => throw new ArgumentException()
        };
    }
}
