using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal class Day07 : BaseSolution
{
    private class Instruction
    {
        public IOperand Operand { get; init; } = null!;
        public string Target { get; init; }
    }

    private struct MemoryAddressOrConstant
    {
        public int Constant { get; }
        public string? MemoryAddress { get; }
        public bool IsMemoryAddress => MemoryAddress is not null;
        private MemoryAddressOrConstant(int constant)
        {
            Constant = constant;
            MemoryAddress = null;
        }

        private MemoryAddressOrConstant(string memoryAddress)
        {
            Constant = -1;
            MemoryAddress = memoryAddress;
        }

        public int GetValue(IReadOnlyDictionary<string, int> memory)
        {
            if(IsMemoryAddress)
            {
                return memory[MemoryAddress!];
            }
            else
            {
                return Constant;
            }
        }

        public static implicit operator MemoryAddressOrConstant(int constant) => new(constant);
        public static implicit operator MemoryAddressOrConstant(string memoryAddress) => new(memoryAddress);
    }

    private interface IOperand
    {
        int GetResult(IReadOnlyDictionary<string, int> values);
        bool CanPerform(IReadOnlyDictionary<string, int> values);
    }

    private class ConstantOperand : IOperand
    {
        private readonly int constantValue;

        public ConstantOperand(int constantValue)
        {
            this.constantValue = constantValue;
        }

        public int GetResult(IReadOnlyDictionary<string, int> values)
        {
            return constantValue;
        }

        public bool CanPerform(IReadOnlyDictionary<string, int> values) => true;
    }

    private class TwoValueOperand : IOperand
    {
        private readonly MemoryAddressOrConstant left;
        private readonly MemoryAddressOrConstant right;
        private readonly Func<int, int, int> operand;

        public TwoValueOperand(
            MemoryAddressOrConstant left,
            MemoryAddressOrConstant right,
            Func<int, int, int> operand
        )
        {
            this.left = left;
            this.right = right;
            this.operand = operand;
        }

        public bool CanPerform(IReadOnlyDictionary<string, int> values)
        {
            return (!left.IsMemoryAddress || values.ContainsKey(left.MemoryAddress!))
                && (!right.IsMemoryAddress || values.ContainsKey(right.MemoryAddress!));
        }

        public int GetResult(IReadOnlyDictionary<string, int> values)
        {
            return operand(left.GetValue(values), right.GetValue(values));
        }
    }

    private class NotOperand : IOperand
    {
        private readonly MemoryAddressOrConstant left;

        public NotOperand(MemoryAddressOrConstant left)
        {
            this.left = left;
        }

        public bool CanPerform(IReadOnlyDictionary<string, int> values)
        {
            return !left.IsMemoryAddress || values.ContainsKey(left.MemoryAddress!);
        }

        public int GetResult(IReadOnlyDictionary<string, int> values)
        {
            return ~(left.GetValue(values));
        }
    }

    private class CopyMemoryAddressOperand : IOperand
    {
        private readonly string memoryAddress;

        public CopyMemoryAddressOperand(string memoryAddress)
        {
            this.memoryAddress = memoryAddress;
        }

        public bool CanPerform(IReadOnlyDictionary<string, int> values)
        {
            return values.ContainsKey(memoryAddress);
        }

        public int GetResult(IReadOnlyDictionary<string, int> values)
        {
            return values[memoryAddress];
        }
    }

    public Day07()
        : base("Some Assembly Required", 2015, 7)
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
        var newMemory = new Dictionary<string, int>()
        {
            { "b", memoryAfterFirstRun["a"] }
        };
        var instructionsWithoutBTArget = instructions.Where(instruction => instruction.Target != "b");
        var finalMemory = RunInstructionsAndProvideAddressA(instructionsWithoutBTArget, newMemory);
        return finalMemory["a"];
    }

    private Dictionary<string, int> RunInstructionsAndProvideAddressA(IEnumerable<Instruction> instructions, Dictionary<string, int>? memory = null)
    {
        memory ??= new();
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

    private static readonly Regex instructionRegex = new Regex(@"(.*) -> ([a-z]{1,2})", RegexOptions.Compiled);
    private IEnumerable<Instruction> DecodeInstructions(FileReader reader)
    {
        return reader.ReadLineByLine()
            .Select(line =>
            {
                var operation = instructionRegex.Match(line);
                if (!operation.Success)
                {
                    throw new InvalidOperationException("Invalid parsed line");
                }

                var operand = operation.Groups[1].Value;
                var targetAddress = operation.Groups[2].Value;

                return new Instruction
                {
                    Target = targetAddress,
                    Operand = DetermineOperand(operand)
                };
            });
    }

    private static readonly Regex numbersOnlyRegex = new(@"^\d+$");
    private static readonly Regex isLeftAndRightOperation = new(@"(\w+) (AND|OR|[LR]SHIFT) (\w+)");
    private static readonly Regex notRegex = new(@"NOT ([a-z\d]+)");
    private static readonly Regex memoryAddressOnly = new(@"^[a-z]+$");


    private IOperand DetermineOperand(string operandAsString)
    {
        if (numbersOnlyRegex.IsMatch(operandAsString))
        {
            return new ConstantOperand(int.Parse(operandAsString));
        }
        var leftAndRightOperand = isLeftAndRightOperation.Match(operandAsString);
        if(leftAndRightOperand.Success)
        {
            var left = DetermineValue(leftAndRightOperand.Groups[1].Value);
            var right = DetermineValue(leftAndRightOperand.Groups[3].Value);
            return new TwoValueOperand(
              left,
              right,
              leftAndRightOperand.Groups[2].Value switch
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
        if(notMatch.Success)
        {
            var left = DetermineValue(notMatch.Groups[1].Value);

            return new NotOperand(left);
        }

        var memoryAddressMatch = memoryAddressOnly.Match(operandAsString);
        if(memoryAddressMatch.Success)
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
            else
            {
                return valueAsString;
            }
        }

        static int And(int left, int right) => left & right;
        static int Or(int left, int right) => left | right;
        static int LeftShift(int left, int right) => left << right;
        static int RightShift(int left, int right) => left >> right;
    }
}
