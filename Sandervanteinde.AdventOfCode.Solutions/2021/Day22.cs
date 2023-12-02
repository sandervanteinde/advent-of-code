using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal class Day22 : BaseSolution
{
    public Day22()
        : base("Reactor Reboot", year: 2021, day: 22)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var instructions = ParseInstruction(reader)
            .Where(
                instruction =>
                    instruction.Area.Left >= -50 && instruction.Area.Right <= 50
                    && instruction.Area.Top >= -50 && instruction.Area.Bottom <= 50
                    && instruction.Area.Front >= -50 && instruction.Area.Back <= 50
            );
        return CalculateTotalOnPoints(instructions.ToArray());
        //var hashset = new HashSet<Point3D>();
        //foreach (var (onOrOff, area) in instructions)
        //{
        //    Func<Point3D, bool> action = onOrOff ? hashset.Add : hashset.Remove;
        //    foreach (var point in area.IterateInclusive())
        //    {
        //        action(point);
        //    }
        //}
        //return hashset.Count;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var instructions = ParseInstruction(reader)
            .ToArray();

        return CalculateTotalOnPoints(instructions);
    }

    private long CalculateTotalOnPoints(Instruction[] instructions)
    {
        var activeAreas = new HashSet<Area3D>();

        foreach (var (isOn, area) in instructions)
        {
            ProcessArea(in area, isOn);
        }

        void ProcessArea(in Area3D area, bool isOn, int counter = 0)
        {
            if (counter == 200)
            {
                throw new InvalidOperationException("To deep");
            }

            foreach (var activeArea in activeAreas)
            {
                if (activeArea == area)
                {
                    if (!isOn)
                    {
                        activeAreas.Remove(area);
                    }

                    return;
                }

                if (activeArea.HasOverlapWith(in area, out var overlap))
                {
                    if (overlap != activeArea)
                    {
                        activeAreas.Remove(activeArea);
                        var splitUpAreas = activeArea.SplitToFit(in overlap);

                        foreach (var splitUpArea in splitUpAreas)
                        {
                            activeAreas.Add(splitUpArea);
                        }
                    }

                    if (overlap == area)
                    {
                        if (!isOn)
                        {
                            activeAreas.Remove(area);
                        }

                        return;
                    }

                    var sourceSplitUpAreas = area.SplitToFit(in overlap);

                    foreach (var splitUpArea in sourceSplitUpAreas)
                    {
                        ProcessArea(in splitUpArea, isOn, counter + 1);
                    }

                    return;
                }
            }

            if (isOn)
            {
                activeAreas.Add(area);
            }
        }

        return activeAreas.Sum(area => area.VolumeInclusive());
    }

    public static IEnumerable<Instruction> ParseInstruction(FileReader reader)
    {
        var regex = new Regex(@"(on|off) x=(-?\d+)\.\.(-?\d+),y=(-?\d+)\.\.(-?\d+),z=(-?\d+)\.\.(-?\d+)");

        foreach (var match in reader.MatchLineByLine(regex))
        {
            var onOrOff = match.Groups[groupnum: 1].Value == "on";
            var minX = int.Parse(match.Groups[groupnum: 2].Value);
            var maxX = int.Parse(match.Groups[groupnum: 3].Value);
            var minY = int.Parse(match.Groups[groupnum: 4].Value);
            var maxY = int.Parse(match.Groups[groupnum: 5].Value);
            var minZ = int.Parse(match.Groups[groupnum: 6].Value);
            var maxZ = int.Parse(match.Groups[groupnum: 7].Value);

            yield return new Instruction(onOrOff, new Area3D(minX, maxX, minY, maxY, minZ, maxZ));
        }
    }

    public record Instruction(bool OnOrOff, Area3D Area);
}
