namespace Sandervanteinde.AdventOfCode.Solutions._2022;
internal class Day05 : BaseSolution
{
    public Day05()
        : base("Supply stacks", 2022, 5)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var input = ParsePuzzleInput(reader);
        foreach(var instruction in input.Instructions)
        {
            input.WareHouse.Move(instruction);
        }

        return input.WareHouse.ReadTopRow();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var input = ParsePuzzleInput(reader);
        foreach (var instruction in input.Instructions)
        {
            input.WareHouse.MoveOrderIntact(instruction);
        }

        return input.WareHouse.ReadTopRow();
    }

    private static PuzzleInput ParsePuzzleInput(FileReader reader)
    {
        var myWarehouse = new WareHouse(new Dictionary<int, Stack<char>>
        {
            [1] = new Stack<char>(new[] { 'S', 'Z', 'P', 'D', 'L', 'B', 'F', 'C' }),
            [2] = new Stack<char>(new[] { 'N', 'V', 'G', 'P', 'H', 'W', 'B' }),
            [3] = new Stack<char>(new[] { 'F', 'W', 'B', 'J', 'G' }),
            [4] = new Stack<char>(new[] {'G', 'J', 'N', 'F', 'L', 'W', 'C', 'S'}),
            [5] = new Stack<char>(new[] { 'W', 'J', 'L', 'T', 'P', 'M', 'S', 'H'}),
            [6] = new Stack<char>(new[] { 'B', 'C', 'W', 'G', 'F', 'S'}),
            [7] = new Stack<char>(new[] { 'H', 'T', 'P', 'M', 'Q', 'B', 'W' }),
            [8] = new Stack<char>(new[] { 'F', 'S', 'W', 'T'}),
            [9] = new Stack<char>(new[] { 'N', 'C', 'R'})
        });

        var instructions = new LinkedList<Instruction>();

        foreach(var line in reader.ReadLineByLine())
        {
            if (line is not ['m', 'o', 'v', 'e', ' ', ..var amount, ' ', 'f', 'r', 'o', 'm', ' ', var from, ' ', 't', 'o', ' ', var to])
            {
                throw new Exception("Unknown line");
            }
            instructions.AddLast(new Instruction { Amount = int.Parse(amount), From = from - 48, To = to - 48 });
        }
        return new PuzzleInput
        {
            WareHouse = myWarehouse,
            Instructions = instructions
        };
    }

    private class PuzzleInput
    {
        public required WareHouse WareHouse { get; init; }
        public required IReadOnlyCollection<Instruction> Instructions { get; internal set; }
    }

    private class Instruction
    {
        public required int From { get; init; }
        public required int To { get; init; }
        public required int Amount { get; init; }
    }

    private class WareHouse
    {
        private readonly Dictionary<int, Stack<char>> items;
        private readonly char[] buffer = new char[100];
        public WareHouse(Dictionary<int, Stack<char>> items)
        {
            this.items = items;
        }

        public void Move(Instruction instruction)
        {
            var fromStack = items[instruction.From];
            var toStack = items[instruction.To];

            for(var i = 0; i < instruction.Amount; i++)
            {
                toStack.Push(fromStack.Pop());
            }
        }

        public void MoveOrderIntact(Instruction instruction)
        {
            var fromStack = items[instruction.From];
            var toStack = items[instruction.To];
            for(var i = 0; i < instruction.Amount; i++)
            {
                buffer[i] = fromStack.Pop();
            }

            for(var i = instruction.Amount - 1; i >= 0; i--)
            {
                toStack.Push(buffer[i]);
            }
        }

        public string ReadTopRow()
        {
            var chars = new char[9];
            for(var i = 1; i <= 9; i++)
            {
                chars[i - 1] = items[i].Peek();
            }

            return new string(chars);
        }
    }
}
