using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal class Day02 : BaseSolution
{
    public Day02()
        : base("Dive!", 2021, 2)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var movements = ParseInputToMovements(reader).ToArray();
        var horizontal = 0;
        var vertical = 0;
        foreach (var movement in movements)
        {
            switch (movement.Direction)
            {
                case Direction.down: vertical += movement.Amount; break;
                case Direction.up: vertical -= movement.Amount; break;
                case Direction.forward: horizontal += movement.Amount; break;
                default: throw new InvalidEnumArgumentException();
            }
        }
        return horizontal * vertical;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var movements = ParseInputToMovements(reader).ToArray();
        var horizontal = 0;
        var vertical = 0;
        var aim = 0;
        foreach (var movement in movements)
        {
            switch (movement.Direction)
            {
                case Direction.down: aim += movement.Amount; break;
                case Direction.up: aim -= movement.Amount; break;
                case Direction.forward:
                    horizontal += movement.Amount;
                    vertical += aim * movement.Amount;
                    break;
                default: throw new InvalidEnumArgumentException();
            }
        }
        return horizontal * vertical;
    }

    private static IEnumerable<Movement> ParseInputToMovements(FileReader reader)
    {
        var regex = new Regex(@"(forward|up|down) (\d+)");
        foreach (var match in reader.MatchLineByLine(regex))
        {
            yield return new Movement
            {
                Amount = int.Parse(match.Groups[2].Value),
                Direction = Enum.Parse<Direction>(match.Groups[1].Value)
            };
        }
    }

    private class Movement
    {
        public int Amount { get; init; }
        public Direction Direction { get; init; }

        public override string ToString()
        {
            return $"{Direction} {Amount}";
        }
    }

    private enum Direction
    {
        forward,
        down,
        up
    }
}
