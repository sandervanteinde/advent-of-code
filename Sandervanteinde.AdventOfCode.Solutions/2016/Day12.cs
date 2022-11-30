using System.Diagnostics.CodeAnalysis;

namespace Sandervanteinde.AdventOfCode.Solutions._2016;
public class Day12 : BaseSolution
{
    public Day12()
        : base("Radioisotope Thermoelectric Generators", 2016, 12)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var lines = reader.ReadLineByLine()
            .Select(ParseOperation)
            .Cast<IOperation>()
            .ToArray();

        var computer = new Computer(lines);

        computer.Execute();

        return computer.ReadMemoryAddress("a");
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var lines = reader.ReadLineByLine()
            .Select(ParseOperation)
            .Cast<IOperation>()
            .ToArray();

        var computer = new Computer(lines);
        computer.WriteMemoryAddress("c", 1);

        computer.Execute();

        return computer.ReadMemoryAddress("a");
    }

    private static object ParseOperation(string s)
    {
        return s.Split(' ') switch
        {
            ["cpy", var firstArg, var secondArg] => new CopyOperation { Source = firstArg, Target = secondArg },
            ["inc", var incArgument] => new IncrementOperation { Target = incArgument },
            ["dec", var decArgument] => new DecrementOperation { Target = decArgument },
            ["jnz", var address, var jumpValue] => new JumpOperation { Address = address, JumpCount = int.Parse(jumpValue) }
        };
    }
}

file interface IOperation
{
    void Process(Computer computer);
}

file class Computer
{
    private readonly IList<IOperation> operations;
    private int instructionPointer = 0;
    private readonly Dictionary<string, int> memory = new();

    public Computer(IList<IOperation> operations)
    {
        this.operations = operations;
    }

    public void Execute()
    {
        while(instructionPointer < operations.Count)
        {
            var currentPointer = instructionPointer;
            operations[instructionPointer].Process(this);
            if(currentPointer == instructionPointer)
            {
                instructionPointer++;
            }
        }
    }

    public int ReadMemoryAddress(string address)
    {
        memory.TryGetValue(address, out var value);
        return value;
    }

    public void WriteMemoryAddress(string address, int value)
    {
        memory[address] = value;
    }

    public void Jump(int jumpCount)
    {
        instructionPointer += jumpCount;
    }
}

file class Address
{
    [MemberNotNullWhen(true, nameof(MemoryAddress))]
    [MemberNotNullWhen(false, nameof(Constant))]
    public required bool IsMemoryAddress { get; init; }
    public string? MemoryAddress { get; init; }
    public int? Constant { get; init; }

    public static implicit operator Address(string input)
    {
        if (int.TryParse(input, out var integerValue))
        {
            return new Address { IsMemoryAddress = false, Constant = integerValue };
        }
        return new Address { IsMemoryAddress = true, MemoryAddress = input };
    }
}

file class CopyOperation : IOperation
{
    public required Address Source { get; init; }
    public required Address Target { get; init; }

    public void Process(Computer computer)
    {
        var value = Source.IsMemoryAddress
            ? computer.ReadMemoryAddress(Source.MemoryAddress)
            : Source.Constant.Value;

        if (!Target.IsMemoryAddress)
        {
            throw new InvalidOperationException("Can't write to constant value");
        }

        computer.WriteMemoryAddress(Target.MemoryAddress, value);
    }
}

file class IncrementOperation : IOperation
{
    public required Address Target { get; init; }

    public void Process(Computer computer)
    {
        if (Target.IsMemoryAddress)
        {
            computer.WriteMemoryAddress(Target.MemoryAddress, computer.ReadMemoryAddress(Target.MemoryAddress) + 1);
        }
    }
}

file class JumpOperation : IOperation
{
    public required Address Address { get; init; }
    public required int JumpCount { get; init; }

    public void Process(Computer computer)
    {
        var evaluateValue = Address.IsMemoryAddress
            ? computer.ReadMemoryAddress(Address.MemoryAddress)
            : Address.Constant.Value;

        if(evaluateValue == 0)
        {
            return;
        }

        computer.Jump(JumpCount);
    }
}

file class DecrementOperation : IOperation
{
    public required Address Target { get; init; }

    public void Process(Computer computer)
    {
        if (Target.IsMemoryAddress)
        {
            computer.WriteMemoryAddress(Target.MemoryAddress, computer.ReadMemoryAddress(Target.MemoryAddress) - 1);
        }
    }
}
