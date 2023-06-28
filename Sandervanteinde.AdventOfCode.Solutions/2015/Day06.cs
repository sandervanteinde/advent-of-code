using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day06 : BaseSolution
{
    public Day06()
        : base("Probably a Fire Hazard", 2015, 6)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var lights = new Dictionary<int, bool>(1000 * 1000);
        for (var x = 0; x <= 999; x++)
        {
            for (var y = 0; y <= 999; y++)
            {
                lights.Add(x * 1000 + y, false);
            }
        }

        foreach (var instruction in ParseInstructions(reader))
        {
            foreach (var point in IteratePoints(instruction.From, instruction.Through))
            {
                var id = point.GetId();
                switch (instruction.Operation)
                {
                    case LightOperation.TurnOn:
                        lights[id] = true;
                        break;
                    case LightOperation.TurnOff:
                        lights[id] = false;
                        break;
                    case LightOperation.Toggle:
                        lights[id] = !lights[id];
                        break;
                }
            }
        }

        return lights.Where(l => l.Value).Count();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var lights = new Dictionary<int, int>(1000 * 1000);
        for (var x = 0; x <= 999; x++)
        {
            for (var y = 0; y <= 999; y++)
            {
                lights.Add(x * 1000 + y, 0);
            }
        }

        foreach (var instruction in ParseInstructions(reader))
        {
            foreach (var point in IteratePoints(instruction.From, instruction.Through))
            {
                var id = point.GetId();
                switch (instruction.Operation)
                {
                    case LightOperation.TurnOn:
                        lights[id] = lights[id] + 1;
                        break;
                    case LightOperation.TurnOff:
                        lights[id] = Math.Max(lights[id] - 1, 0);
                        break;
                    case LightOperation.Toggle:
                        lights[id] = lights[id] + 2;
                        break;
                }
            }
        }

        return lights.Sum(kvp => (uint)kvp.Value);
    }

    private static IEnumerable<Point> IteratePoints(Point from, Point through)
    {
        for (var x = from.X; x <= through.X; x++)
        {
            for (var y = from.Y; y <= through.Y; y++)
            {
                yield return new Point { X = x, Y = y };
            }
        }
    }

    private static IEnumerable<LightInstruction> ParseInstructions(FileReader reader)
    {
        var regex = new Regex("(turn on|turn off|toggle) (\\d+),(\\d+) through (\\d+),(\\d+)", RegexOptions.Compiled);
        return reader.ReadLineByLine()
            .Select(line =>
            {
                var match = regex.Match(line);
                if (!match.Success)
                {
                    throw new NotSupportedException();
                }

                return new LightInstruction
                {
                    Operation = match.Groups[1].Value switch
                    {
                        "turn on" => LightOperation.TurnOn,
                        "turn off" => LightOperation.TurnOff,
                        "toggle" => LightOperation.Toggle,
                        _ => throw new NotSupportedException()
                    },
                    From = new Point
                    {
                        X = int.Parse(match.Groups[2].Value),
                        Y = int.Parse(match.Groups[3].Value)
                    },
                    Through = new Point
                    {
                        X = int.Parse(match.Groups[4].Value),
                        Y = int.Parse(match.Groups[5].Value)
                    }
                };
            });
    }
}