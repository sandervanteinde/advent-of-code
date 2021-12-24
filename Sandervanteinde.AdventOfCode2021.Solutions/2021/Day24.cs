namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day24 : BaseSolution
{
    public Day24()
        : base(@"Arithmetic Logic Unit", 2021, 24)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var instructions = ParseInstructions(reader).ToList();
        var subroutines = instructions.Chunk(instructions.Count / 14)
            .Select((instruction, index) => new SimpleInstruction
            {
                Instruction = instruction,
                Div = instruction[4].Right(),
                Check = instruction[5].Right(),
                Offset = instruction[15].Right(),
                Index = index
            })
            .ToArray();

        var offsets = new long[14];

        var stack = new Stack<SimpleInstruction>();

        foreach (var subroutine in subroutines)
        {
            switch (subroutine.Div)
            {
                case 1:
                    stack.Push(subroutine);
                    break;
                case 26:
                    var pop = stack.Pop();
                    offsets[subroutine.Index] = pop.Offset + subroutine.Check;
                    offsets[pop.Index] = offsets[subroutine.Index] * -1;
                    break;
                default:
                    throw new InvalidOperationException("");
            }
        }

        var sum = 0L;
        foreach (var offset in offsets)
        {
            sum *= 10;
            if (offset < 0)
            {
                sum += (9L + offset);
            }
            else
            {
                sum += 9;
            }
        }

        return sum;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var instructions = ParseInstructions(reader).ToList();
        var subroutines = instructions.Chunk(instructions.Count / 14)
            .Select((instruction, index) => new SimpleInstruction
            {
                Instruction = instruction,
                Div = instruction[4].Right(),
                Check = instruction[5].Right(),
                Offset = instruction[15].Right(),
                Index = index
            })
            .ToArray();

        var offsets = new long[14];

        var stack = new Stack<SimpleInstruction>();

        foreach (var subroutine in subroutines)
        {
            switch (subroutine.Div)
            {
                case 1:
                    stack.Push(subroutine);
                    break;
                case 26:
                    var pop = stack.Pop();
                    offsets[subroutine.Index] = pop.Offset + subroutine.Check;
                    offsets[pop.Index] = offsets[subroutine.Index] * -1;
                    break;
                default:
                    throw new InvalidOperationException("");
            }
        }

        var sum = 0L;
        foreach (var offset in offsets)
        {
            sum *= 10;
            if (offset < 0)
            {
                sum += 1;
            }
            else
            {
                sum += 1 + offset;
            }
        }

        return sum;
    }

    private static IEnumerable<Instruction> ParseInstructions(FileReader reader)
    {
        return reader.ReadLineByLine()
            .Select(line => new Instruction(line))
            .ToArray();


    }
}
