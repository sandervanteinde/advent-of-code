namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day24
{
    public class Instruction
    {
        private readonly Func<ComputerValues, long> left;
        private readonly Func<ComputerValues, long>? right;
        private readonly Action<ComputerValues, long> setter;

        public Instruction(string instruction)
        {
            var parsed = instruction.Split(separator: ' ');
            Operation = parsed[0] switch
            {
                "inp" => new InputOperation(),
                "add" => new AddOperation(),
                "mul" => new MultiplyOperation(),
                "div" => new DivideOperation(),
                "mod" => new ModOperation(),
                "eql" => new EqlOperation(),
                _ => throw new InvalidOperationException("Invalid input")
            };
            left = ParseInstruction(parsed[1]);
            right = parsed.Length == 2
                ? null
                : ParseInstruction(parsed[2]);
            setter = ParseSetter(parsed[1]);
            RawInstruction = instruction;
        }

        public IOperation Operation { get; }
        public string RawInstruction { get; }

        public long Right()
        {
            return right!(new ComputerValues());
        }

        public void PerformInstruction(ComputerValues values, int[] serialNumber, ref int counter)
        {
            if (Operation is InputOperation)
            {
                setter(values, serialNumber[counter++]);
                return;
            }

            setter(values, Operation.PerformOperation(left(values), right!(values)));
        }

        private static Action<ComputerValues, long> ParseSetter(string str)
        {
            return str switch
            {
                "w" => (oldValue, newValue) => oldValue.W = newValue,
                "x" => (oldValue, newValue) => oldValue.X = newValue,
                "y" => (oldValue, newValue) => oldValue.Y = newValue,
                "z" => (oldValue, newValue) => oldValue.Z = newValue,
                _ => throw new NotSupportedException("Can't store a number in a number")
            };
        }

        public string ParsedForValues(ComputerValues values, int[] serialNumber, int counter)
        {
            if (Operation is InputOperation)
            {
                return $"Ser# {serialNumber[counter]}";
            }

            var operatorSymbol = Operation switch
            {
                AddOperation => '+',
                MultiplyOperation => '*',
                DivideOperation => '/',
                ModOperation => '%',
                EqlOperation => '=',
                _ => throw new NotSupportedException()
            };

            return $"{left(values)} {operatorSymbol} {right!(values)}";
        }

        private static Func<ComputerValues, long> ParseInstruction(string str)
        {
            return str switch
            {
                "w" => values => values.W,
                "x" => values => values.X,
                "y" => values => values.Y,
                "z" => values => values.Z,
                _ => AsLong()
            };

            Func<ComputerValues, long> AsLong()
            {
                var asLong = long.Parse(str);
                return _ => asLong;
            }
        }
    }
}
