using System.Text;

namespace Sandervanteinde.AdventOfCode.Solutions._2016;

internal partial class Day02 : BaseSolution
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
        var output = new StringBuilder();
        var position = new PassCodePosition();
        foreach (var line in reader.ReadLineByLine())
        {
            foreach (var c in line)
            {
                position = c switch
                {
                    'U' => position.MoveUp(),
                    'D' => position.MoveDown(),
                    'L' => position.MoveLeft(),
                    'R' => position.MoveRight(),
                    _ => throw new NotSupportedException("Invalid char in input")
                };
            }
            output.Append(position.GetPasscodeChar());
        }
        return output.ToString();
    }
}
