﻿using System.Numerics;

namespace Sandervanteinde.AdventOfCode.Solutions.Utils;

public record struct Point : IAdditionOperators<Point, Point, Point>
{
    public int X { get; init; }
    public int Y { get; init; }

    public static Point operator +(Point left, Point right)
    {
        return left with { X = left.X + right.X, Y = left.Y + right.Y };
    }

    public int GetId()
    {
        return (X * 1000) + Y;
    }

    public int DistanceFromOrigin()
    {
        return Math.Abs(X) + Math.Abs(Y);
    }

    public static IEnumerable<Point> BetweenExclusiveStartInclusiveEnd(Point start, Point end)
    {
        var startX = start.X < end.X
            ? start.X
            : end.X;
        var endX = startX == start.X
            ? end.X
            : start.X;
        var startY = start.Y < end.Y
            ? start.Y
            : end.Y;
        var endY = startY == start.Y
            ? end.Y
            : start.Y;

        for (var x = startX; x <= endX; x++)
        {
            for (var y = startY; y <= endY; y++)
            {
                var point = new Point { X = x, Y = y };

                if (point == start)
                {
                    continue;
                }

                yield return new Point { X = x, Y = y };
            }
        }
    }

    public static int ManhattanDistance(Point left, Point right)
    {
        return Math.Abs(left.X - right.X) + Math.Abs(left.Y - right.Y);
    }

    public static IEnumerable<Point> BetweenInclusive(Point start, Point end)
    {
        var startX = start.X < end.X
            ? start.X
            : end.X;
        var endX = startX == start.X
            ? end.X
            : start.X;
        var startY = start.Y < end.Y
            ? start.Y
            : end.Y;
        var endY = startY == start.Y
            ? end.Y
            : start.Y;

        for (var x = startX; x <= endX; x++)
        {
            for (var y = startY; y <= endY; y++)
            {
                yield return new Point { X = x, Y = y };
            }
        }
    }

    public static IEnumerable<Point> DiagonalInclusive(Line line)
    {
        if (!line.IsDiagonal())
        {
            throw new InvalidOperationException("Line is not a diagonal");
        }

        var diff = Math.Abs(line.Start.X - line.End.X);
        var isXGoingUp = line.End.X > line.Start.X;
        var isYGOingUp = line.End.Y > line.Start.Y;
        var p = line.Start;

        for (var i = 0; i <= diff; i++)
        {
            yield return p;
            p = p with
            {
                X = isXGoingUp
                    ? p.X + 1
                    : p.X - 1,
                Y = isYGOingUp
                    ? p.Y + 1
                    : p.Y - 1
            };
        }
    }

    /// <summary>
    ///     Checks if <paramref name="other" /> is next to this point.
    ///     It is considered adjacent when it is diagonally, horizontally or vertically one space further.
    ///     When on top it is also true.
    /// </summary>
    public bool IsAdjacentOrOnTop(Point other)
    {
        return Math.Sqrt(Math.Pow(Math.Abs(other.X - X), y: 2) + Math.Pow(Math.Abs(other.Y - Y), y: 2)) <= 1.415;
    }
}
