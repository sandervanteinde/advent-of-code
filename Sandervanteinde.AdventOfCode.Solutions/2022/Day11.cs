namespace Sandervanteinde.AdventOfCode.Solutions._2022;

public class Day11 : BaseSolution
{
    public Day11()
        : base("Monkey in the Middle", 2022, 11)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        Monkey.HasWorryDivision = true;
        var monkeys = ParseMonkeys(reader);

        for(var round = 0; round < 20; round++)
        {
            foreach(var monkey in monkeys)
            {
                foreach(var (itemThrown, targetMonkey) in monkey.Inspect())
                {
                    monkeys[targetMonkey].Items.Add(itemThrown);
                }
            }
        }

        return monkeys
            .OrderByDescending(monkey => monkey.InspectCount)
            .Take(2)
            .Aggregate(1, (val, monkey) => val * monkey.InspectCount);
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        Monkey.HasWorryDivision = false;
        var monkeys = ParseMonkeys2(reader);

        foreach(var item in monkeys.SelectMany(x => x.Items))
        {
            foreach(var monkey in monkeys)
            {
                item.AddDividableTracker(monkey.Divisor);
            }
        }

        for (var round = 0; round < 10_000; round++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var (itemThrown, targetMonkey) in monkey.Inspect())
                {
                    monkeys[targetMonkey].Items.Add(itemThrown);
                }
            }
        }

        return monkeys
            .OrderByDescending(monkey => monkey.InspectCount)
            .Take(2)
            .Select(x => (ulong)x.InspectCount)
            .Aggregate(1UL, (val, monkey) => val * monkey);
    }

    private static Monkey[] ParseMonkeys(FileReader reader)
    {
        List<int> monkeyItems = new List<int>();
        Func<int, int> monkeyOperation = x => x;
        Predicate<int> monkeyTest = _ => false;
        int trueMonkey = 0, falseMonkey = 0;
        List<Monkey> monkeys = new();

        foreach(var line in reader.ReadAsSpanLineByLine())
        {
            switch (line)
            {
                case ['M', 'o', 'n', 'k', 'e', 'y', ' ', _, ':']:
                    break;
                case [' ', ' ', 'S', 't', 'a', 'r', 't', 'i', 'n', 'g', ' ', 'i', 't', 'e', 'm', 's', ':', ' ', .. var items ]:
                    monkeyItems.AddRange(items.ToString().Split(", ").Select(int.Parse));
                    break;
                case [' ', ' ', 'O', 'p', 'e', 'r', 'a', 't', 'i', 'o', 'n', ':', ' ', 'n', 'e', 'w', ' ', '=', ' ', 'o', 'l', 'd', ' ', .. var op]:
                    monkeyOperation = op switch
                    {
                        ['+', ' ', .. var plusAmount] => CreateAddOperation(plusAmount),
                        ['*', ' ', .. var multiAmount] => CreateMultiplyOperation(multiAmount),
                        _ => throw new NotSupportedException()
                    };
                    break;
                case [' ', ' ', 'T', 'e', 's', 't', ':', ' ', 'd', 'i', 'v', 'i', 's', 'i', 'b', 'l', 'e', ' ', 'b', 'y', ' ', .. var divisibleByAmount]:
                    monkeyTest = CreateModuloTest(divisibleByAmount);   
                    break;
                case [' ', ' ', ' ', ' ', 'I', 'f', ' ', .. var trueOrFalse, ':', ' ', 't', 'h', 'r', 'o', 'w', ' ', 't', 'o', ' ', 'm', 'o', 'n', 'k', 'e', 'y', ' ', var targetMonkey]:
                    if(trueOrFalse is "true")
                    {
                        trueMonkey = (targetMonkey - 48);
                    }
                    else
                    {
                        falseMonkey = (targetMonkey - 48);
                    }
                    break;
                case []:
                    AddMonkey();
                    break;
                default:
                    throw new InvalidOperationException("Invalid line");

            }
        }

        AddMonkey();
        return monkeys.ToArray();

        void AddMonkey()
        {
            monkeys.Add(new Monkey
            {
                FalseMonkey = falseMonkey,
                Items = monkeyItems,
                Operation = monkeyOperation,
                Test = monkeyTest,
                TrueMonkey = trueMonkey,
            });
            falseMonkey = 0;
            monkeyItems = new();
        }

        static Func<int, int> CreateAddOperation(ReadOnlySpan<char> amount)
        {
            if(amount is "old")
            {
                return x => x + x;
            }
            var integerAMount = int.Parse(amount);
            return x => x + integerAMount;
        }

        static Func<int, int> CreateMultiplyOperation(ReadOnlySpan<char> amount)
        {
            if(amount is "old")
            {
                return x => x * x;
            }
            int integerAmount = int.Parse(amount);
            return x => x * integerAmount;
        }

        static Predicate<int> CreateModuloTest(ReadOnlySpan<char> amount)
        {
            var integerAmount = int.Parse(amount);
            return x => x % integerAmount == 0;
        }
    }
    private static Monkey2[] ParseMonkeys2(FileReader reader)
    {
        int monkeyId = 0;
        List<MonkeyItem> monkeyItems = new List<MonkeyItem>();
        Action<MonkeyItem> monkeyOperation = _ => { };
        int trueMonkey = 0, falseMonkey = 0;
        var divisor = 0;
        List<Monkey2> monkeys = new();

        foreach (var line in reader.ReadAsSpanLineByLine())
        {
            switch (line)
            {
                case ['M', 'o', 'n', 'k', 'e', 'y', ' ', var id, ':']:
                    monkeyId = id - 48;
                    break;
                case [' ', ' ', 'S', 't', 'a', 'r', 't', 'i', 'n', 'g', ' ', 'i', 't', 'e', 'm', 's', ':', ' ', .. var items]:
                    monkeyItems.AddRange(items.ToString().Split(", ").Select(value => new MonkeyItem(int.Parse(value))));
                    break;
                case [' ', ' ', 'O', 'p', 'e', 'r', 'a', 't', 'i', 'o', 'n', ':', ' ', 'n', 'e', 'w', ' ', '=', ' ', 'o', 'l', 'd', ' ', .. var op]:
                    monkeyOperation = op switch
                    {
                        ['+', ' ', .. var plusAmount] => CreateAddOperation(plusAmount),
                        ['*', ' ', .. var multiAmount] => CreateMultiplyOperation(multiAmount),
                        _ => throw new NotSupportedException()
                    };
                    break;
                case [' ', ' ', 'T', 'e', 's', 't', ':', ' ', 'd', 'i', 'v', 'i', 's', 'i', 'b', 'l', 'e', ' ', 'b', 'y', ' ', .. var divisibleByAmount]:
                    divisor = int.Parse(divisibleByAmount);
                    break;
                case [' ', ' ', ' ', ' ', 'I', 'f', ' ', .. var trueOrFalse, ':', ' ', 't', 'h', 'r', 'o', 'w', ' ', 't', 'o', ' ', 'm', 'o', 'n', 'k', 'e', 'y', ' ', var targetMonkey]:
                    if (trueOrFalse is "true")
                    {
                        trueMonkey = (targetMonkey - 48);
                    }
                    else
                    {
                        falseMonkey = (targetMonkey - 48);
                    }
                    break;
                case []:
                    AddMonkey();
                    break;
                default:
                    throw new InvalidOperationException("Invalid line");

            }
        }

        AddMonkey();
        return monkeys.ToArray();

        void AddMonkey()
        {
            monkeys.Add(new Monkey2
            {
                FalseMonkey = falseMonkey,
                Id = monkeyId,
                Items = monkeyItems,
                Operation = monkeyOperation,
                TrueMonkey = trueMonkey,
                Divisor = divisor
            });
            falseMonkey = 0;
            monkeyId = 0;
            monkeyItems = new();
        }

        static Action<MonkeyItem> CreateAddOperation(ReadOnlySpan<char> amount)
        {
            if (amount is "old")
            {
                throw new NotSupportedException();
            }
            var integerAMount = int.Parse(amount);
            return x => x.Add(integerAMount);
        }

        static Action<MonkeyItem> CreateMultiplyOperation(ReadOnlySpan<char> amount)
        {
            if (amount is "old")
            {
                return x => x.MultiplySelf();
            }
            int integerAmount = int.Parse(amount);
            return x => x.Multiply(integerAmount);
        }
    }

    private class Monkey
    {
        public required List<int> Items { get; init; }
        public required Func<int, int> Operation { get; init; }
        public required Predicate<int> Test { get; init; }
        public required int TrueMonkey { get; init; }
        public required int FalseMonkey { get; init; }
        public static bool HasWorryDivision { get; set; } = true;

        public int InspectCount { get; private set; }

        public IEnumerable<(int item, int targetMonkey)> Inspect()
        {
            foreach(var item in Items)
            {
                InspectCount++;
                int newValue = Operation.Invoke(item);
                if (HasWorryDivision)
                {
                    newValue /= 3;
                }
                else
                {
                    newValue &= 255;
                }

                var targetMonkey = Test(newValue) ? TrueMonkey : FalseMonkey;
                yield return (newValue, targetMonkey);
            }

            Items.Clear();
        }
    }

    private class MonkeyItem
    {
        private readonly int _value;
        private readonly List<int> _dividers = new List<int>();
        private readonly List<int> _currentRemainders = new List<int>();
        private readonly List<bool> _results = new List<bool>();

        public MonkeyItem(int value)
        {
            this._value = value;
        }

        public void AddDividableTracker(int amount)
        {
            _dividers.Add(amount);
            _results.Add(_value % amount == 0);
            _currentRemainders.Add(_value % amount);
        }

        public void Add(int amount)
        {
            for(var i = 0; i < _dividers.Count; i++)
            {
                var newRemainder = _currentRemainders[i] + amount;
                _currentRemainders[i] = newRemainder % _dividers[i];
                _results[i] = _currentRemainders[i] == 0;
            }
        }

        public void Multiply(int amount)
        {
            for (var i = 0; i < _dividers.Count; i++)
            {
                var newRemainder = _currentRemainders[i] * amount;
                _currentRemainders[i] = newRemainder % _dividers[i];
                _results[i] = _currentRemainders[i] == 0;
            }
        }

        public void MultiplySelf()
        {
            for (var i = 0; i < _dividers.Count; i++)
            {
                var newRemainder = _currentRemainders[i] * _currentRemainders[i];
                _currentRemainders[i] = newRemainder % _dividers[i];
                _results[i] = _currentRemainders[i] == 0;
            }
        }

        public bool IsTrue(int index) => _results[index];
    }

    private class Monkey2
    {
        public required int Id { get; init; }
        public required List<MonkeyItem> Items { get; init; }
        public required Action<MonkeyItem> Operation { get; init; }
        public required int TrueMonkey { get; init; }
        public required int FalseMonkey { get; init; }
        public required int Divisor { get; init; }

        public int InspectCount { get; private set; }

        public IEnumerable<(MonkeyItem item, int targetMonkey)> Inspect()
        {
            foreach (var item in Items)
            {
                InspectCount++;
                Operation.Invoke(item);
                var targetMonkey = item.IsTrue(Id) ? TrueMonkey : FalseMonkey;
                yield return (item, targetMonkey);
            }

            Items.Clear();
        }
    }

}
