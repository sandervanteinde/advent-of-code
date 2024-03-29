﻿using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day23 : BaseSolution
{
    public Day23()
        : base("Opening the Turing Lock", year: 2015, day: 23)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var instructions = ParseInstructions(reader);
        var computer = new Computer(instructions.ToList());

        while (computer.IsValidInstruction)
        {
            var instruction = computer.CurrentInstruction;
            instruction.PerformInstruction(computer);

            if (instruction.ShouldAutoIncrement())
            {
                computer.ApplyIndexOffset(offset: 1);
            }
        }

        return computer.RegisterB;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var instructions = ParseInstructions(reader);
        var computer = new Computer(instructions.ToList()) { RegisterA = 1 };

        while (computer.IsValidInstruction)
        {
            var instruction = computer.CurrentInstruction;
            instruction.PerformInstruction(computer);

            if (instruction.ShouldAutoIncrement())
            {
                computer.ApplyIndexOffset(offset: 1);
            }
        }

        return computer.RegisterB;
    }

    private IEnumerable<IInstruction> ParseInstructions(FileReader reader)
    {
        var regex = new Regex(@"(hlf|tpl|inc|jmp|jie|jio) (.+)");

        foreach (var match in reader.MatchLineByLine(regex))
        {
            yield return match.Groups[groupnum: 1].Value switch
            {
                "hlf" => new HalfInstruction(),
                "tpl" => new TripleInstruction(),
                "inc" => new IncrementInstruction(
                    match.Groups[groupnum: 2]
                        .Value[index: 0]
                ),
                "jmp" => new JumpInstruction(int.Parse(match.Groups[groupnum: 2].Value), _ => true),
                "jie" => DetermineInstruction(match.Groups[groupnum: 2].Value, isOne: false),
                "jio" => DetermineInstruction(match.Groups[groupnum: 2].Value, isOne: true),
                _ => throw new NotSupportedException()
            };
        }

        IInstruction DetermineInstruction(string input, bool isOne)
        {
            var split = input.Split(", ");
            var register = split[0];
            var offset = int.Parse(split[1]);
            Func<Computer, int> valueDeterminer = register == "a"
                ? cmp => cmp.RegisterA
                : cmp => cmp.RegisterB;
            Predicate<Computer> predicate = isOne
                ? cmp => valueDeterminer(cmp) == 1
                : cmp => valueDeterminer(cmp) % 2 == 0;
            return new JumpInstruction(offset, predicate);
        }
    }
}
