namespace Sandervanteinde.AdventOfCode.Solutions._2022;
public class Day09 : BaseSolution
{
    public Day09()
        : base("Rope Bridge", 2022, 9)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var ownPoint = new Point { X = 0, Y = 0 };
        var tailPoint = new Point { X = 0, Y = 0 };
        var visitedPoints = new HashSet<Point>() { tailPoint };

        foreach (var line in reader.ReadAsSpanLineByLine())
        {
            if (line is not [var direction, ' ', .. var amountAsString])
            {
                throw new InvalidOperationException("Expeced a different line");
            }
            var amount = int.Parse(amountAsString);

            for (var i = 0; i < amount; i++)
            {
                var prevPoint = ownPoint;
                ownPoint = direction switch
                {
                    'L' => ownPoint with { X = ownPoint.X - 1 },
                    'R' => ownPoint with { X = ownPoint.X + 1 },
                    'U' => ownPoint with { Y = ownPoint.Y + 1 },
                    'D' => ownPoint with { Y = ownPoint.Y - 1 },
                    _ => throw new NotSupportedException()
                };

                if (!ownPoint.IsAdjacentOrOnTop(tailPoint))
                {
                    tailPoint = prevPoint;
                    visitedPoints.Add(tailPoint);
                }
            }

        }
        return visitedPoints.Count;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var ownPoint = new Point { X = 0, Y = 0 };
        var tailPoints = Enumerable.Repeat(ownPoint, 9).ToArray();
        var visitedPoints = new HashSet<Point>() { ownPoint };

        foreach (var line in reader.ReadAsSpanLineByLine())
        {
            if (line is not [var direction, ' ', .. var amountAsString])
            {
                throw new InvalidOperationException("Expeced a different line");
            }
            var amount = int.Parse(amountAsString);

            for (var i = 0; i < amount; i++)
            {
                ownPoint = direction switch
                {
                    'L' => ownPoint with { X = ownPoint.X - 1 },
                    'R' => ownPoint with { X = ownPoint.X + 1 },
                    'U' => ownPoint with { Y = ownPoint.Y + 1 },
                    'D' => ownPoint with { Y = ownPoint.Y - 1 },
                    _ => throw new NotSupportedException()
                };


                if (DetermineNewPoint(ref ownPoint, ref tailPoints[0]))
                {
                    for(var tail = 1; tail < 9; tail++)
                    {
                        if (!DetermineNewPoint(ref tailPoints[tail - 1], ref tailPoints[tail]))
                        {
                            break;
                        }
                        if(tail == 8)
                        {
                            visitedPoints.Add(tailPoints[tail]);
                        }
                    }
                }

                //Print(ownPoint, tailPoints);
            }

        }
        return visitedPoints.Count;
    }

    private static bool DetermineNewPoint(ref Point previous, ref Point next)
    {
        if (previous.IsAdjacentOrOnTop(next))
        {
            return false;
        }

        if(previous.X != next.X)
        {
            if (previous.X > next.X)
            {
                next = next with { X = next.X + 1 };
            }
            else
            {
                next = next with { X = next.X - 1 };
            }
        }

        if(previous.Y != next.Y)
        {
            if (previous.Y > next.Y)
            {
                next = next with { Y = next.Y + 1 };
            }
            else
            {
                next = next with { Y = next.Y - 1 };
            }
        }
        return true;
    }

    private static void Print(Point own, Point[] tails)
    {
        Console.Clear();
        var writtenPoints = new HashSet<Point>() { own };
        Console.CursorTop = own.Y + 25;
        Console.CursorLeft = own.X + 50;
        Console.Write('H');

        for(var i = 0; i < tails.Length; i++)
        {
            var tailItem = tails[i];
            if (!writtenPoints.Add(tailItem))
            {
                continue;
            }
            Console.CursorTop = tailItem.Y + 25;
            Console.CursorLeft = tailItem.X + 50;
            Console.Write((char)(i + 49));
        }

    }

    private static void Switcheroo<T>(ref T assign, ref T newValue)
    {
        var oldValue = assign;
        assign = newValue;
        newValue = oldValue;
    }
}
