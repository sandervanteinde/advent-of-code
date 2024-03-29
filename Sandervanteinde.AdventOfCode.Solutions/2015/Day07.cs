﻿using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day07 : BaseSolution
{
    private static readonly Regex instructionRegex = new(@"(.*) -> ([a-z]{1,2})", RegexOptions.Compiled);

    private static readonly Regex numbersOnlyRegex = new(@"^\d+$");
    private static readonly Regex isLeftAndRightOperation = new(@"(\w+) (AND|OR|[LR]SHIFT) (\w+)");
    private static readonly Regex notRegex = new(@"NOT ([a-z\d]+)");
    private static readonly Regex memoryAddressOnly = new(@"^[a-z]+$");

    public Day07()
        : base("Some Assembly Required", year: 2015, day: 7)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        return RunInstructionsAndProvideAddressA(DecodeInstructions(reader))["a"];
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var instructions = DecodeInstructions(reader);
        var memoryAfterFirstRun = RunInstructionsAndProvideAddressA(instructions);
        memoryAfterFirstRun["b"] = memoryAfterFirstRun["a"];
        var newMemory = new Dictionary<string, int> { { "b", memoryAfterFirstRun["a"] } };
        var instructionsWithoutBTArget = instructions.Where(instruction => instruction.Target != "b");
        var finalMemory = RunInstructionsAndProvideAddressA(instructionsWithoutBTArget, newMemory);
        return finalMemory["a"];
    }

    private Dictionary<string, int> RunInstructionsAndProvideAddressA(IEnumerable<Instruction> instructions, Dictionary<string, int>? memory = null)
    {
        memory ??= new Dictionary<string, int>();
        var queue = new Queue<Instruction>();

        foreach (var instruction in instructions)
        {
            if (!instruction.Operand.CanPerform(memory))
            {
                queue.Enqueue(instruction);
                continue;
            }

            memory[instruction.Target] = instruction.Operand.GetResult(memory);
        }

        while (queue.Count > 0)
        {
            var instruction = queue.Dequeue();

            if (!instruction.Operand.CanPerform(memory))
            {
                queue.Enqueue(instruction);
                continue;
            }

            memory[instruction.Target] = instruction.Operand.GetResult(memory);
        }

        return memory;
    }

    private IEnumerable<Instruction> DecodeInstructions(FileReader reader)
    {
        return reader.ReadLineByLine()
            .Select(
                line =>
                {
                    var operation = instructionRegex.Match(line);

                    if (!operation.Success)
                    {
                        throw new InvalidOperationException("Invalid parsed line");
                    }

                    var operand = operation.Groups[groupnum: 1].Value;
                    var targetAddress = operation.Groups[groupnum: 2].Value;

                    return new Instruction { Target = targetAddress, Operand = DetermineOperand(operand) };
                }
            );
    }

    private IOperand DetermineOperand(string operandAsString)
    {
        if (numbersOnlyRegex.IsMatch(operandAsString))
        {
            return new ConstantOperand(int.Parse(operandAsString));
        }

        var leftAndRightOperand = isLeftAndRightOperation.Match(operandAsString);

        if (leftAndRightOperand.Success)
        {
            var left = DetermineValue(leftAndRightOperand.Groups[groupnum: 1].Value);
            var right = DetermineValue(leftAndRightOperand.Groups[groupnum: 3].Value);
            return new TwoValueOperand(
                left,
                right,
                leftAndRightOperand.Groups[groupnum: 2].Value switch
                {
                    "AND" => And,
                    "OR" => Or,
                    "LSHIFT" => LeftShift,
                    "RSHIFT" => RightShift,
                    _ => throw new InvalidOperationException("Unknown operand")
                }
            );
        }

        var notMatch = notRegex.Match(operandAsString);

        if (notMatch.Success)
        {
            var left = DetermineValue(notMatch.Groups[groupnum: 1].Value);

            return new NotOperand(left);
        }

        var memoryAddressMatch = memoryAddressOnly.Match(operandAsString);

        if (memoryAddressMatch.Success)
        {
            return new CopyMemoryAddressOperand(operandAsString);
        }

        throw new InvalidOperationException("Unknown operand");

        static MemoryAddressOrConstant DetermineValue(string valueAsString)
        {
            if (numbersOnlyRegex.IsMatch(valueAsString))
            {
                return int.Parse(valueAsString);
            }

            return valueAsString;
        }

        static int And(int left, int right)
        {
            return left & right;
        }

        static int Or(int left, int right)
        {
            return left | right;
        }

        static int LeftShift(int left, int right)
        {
            return left << right;
        }

        static int RightShift(int left, int right)
        {
            return left >> right;
        }
    }
}
