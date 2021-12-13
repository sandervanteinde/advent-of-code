using System.Text;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2016;

internal class Day02 : BaseSolution
{
    public Day02()
        : base("Bathroom Security", 2016, 2)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var point = new Point { X = 1, Y = 1 };
        var output = new StringBuilder();
        foreach (var line in reader.ReadLineByLine())
        {
            foreach (var c in line)
            {
                point = c switch
                {
                    'U' => point with { Y = Math.Max(0, point.Y - 1) },
                    'D' => point with { Y = Math.Min(2, point.Y + 1) },
                    'L' => point with { X = Math.Max(0, point.X - 1) },
                    'R' => point with { X = Math.Min(2, point.X + 1) },
                    _ => throw new NotSupportedException("Invalid char in input")
                };
            }
            output.Append(point.X + 1 + point.Y * 3);
        }
        return output.ToString();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var point = new Point { X = -2, Y = 0 };
        var output = new StringBuilder();
        foreach (var line in reader.ReadLineByLine())
        {
            foreach (var c in line)
            {
                point = c switch
                {
                    'U' => point with { Y = Math.Max(0, point.Y - 1) },
                    'D' => point with { Y = Math.Min(2, point.Y + 1) },
                    'L' => point with { X = Math.Max(0, point.X - 1) },
                    'R' => point with { X = Math.Min(2, point.X + 1) },
                    _ => throw new NotSupportedException("Invalid char in input")
                };
            }
            output.Append(point.X + 1 + point.Y * 3);
        }
        return output.ToString();
    }

    private class PassCodePosition
    {
        public int X { get; private set; } = -2;
        public int Y { get; private set; } = 0;

        public void MoveUp()
        {
            if (Y == -2)
            {
                return;
            }
            if (Y == 2)
            {
                Y--;
                return;
            }

            if (Y is -1 && X is not 0)
            {
                return;
            }

            if (Y == 0 && X is < -2 or > 2)
            {
                return;
            }
            if (Y == 1 && X is < -1 or > 1)
            {
                return;
            }

            Y--;
        }

        public void MoveDown()
        {
            if (Y == 2)
            {
                return;
            }

            if (Y == -2)
            {
                Y++;
                return;
            }
            if (Y == -1 && X is > 1 or < -1)
            {
                return;
            }
        }

        public void MoveLeft()
        {

        }

        public void MoveRight()
        {

        }

        public char GetPasscodeChar()
        {
            return this switch
            {
                { X: 0, Y: -2 } => '1',
                { X: -1, Y: -1 } => '2',
                { X: 0, Y: -1 } => '3',
                { X: 1, Y: -1 } => '4',
                { X: -2, Y: 0 } => '5',
                { X: -1, Y: 0 } => '6',
                { X: 0, Y: 0 } => '7',
                { X: 1, Y: 0 } => '8',
                { X: 2, Y: 0 } => '9',
                { X: -1, Y: 1 } => 'A',
                { X: 0, Y: 1 } => 'B',
                { X: 1, Y: 1 } => 'C',
                { X: 0, Y: 2 } => 'D',
                _ => throw new NotSupportedException("Invalid location of passcode")
            };
        }
    }
}
