using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day08 : BaseSolution
{
    public Day08()
        : base("Seven Segment Search", 2021, 8)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var outputsWith147or8 = 0;
        foreach (var line in DecodeLines(reader))
        {
            outputsWith147or8 += line.OutputValues.Where(i => i is 1 or 4 or 7 or 8).Count();
        }
        return outputsWith147or8;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var totalSum = 0;
        foreach (var line in DecodeLines(reader))
        {
            totalSum += (line.OutputValues[0] * 1000 + line.OutputValues[1] * 100 + line.OutputValues[2] * 10 + line.OutputValues[3]);
        }
        return totalSum;
    }

    private IEnumerable<DecodedLine> DecodeLines(FileReader reader)
    {
        var mapping = new Dictionary<string, int>
        {
            ["abcef"] = 0,
            ["cf"] = 1,
            ["acdeg"] = 2,
            ["acdfg"] = 3,
            ["bcdf"] = 4,
            ["abdfg"] = 5,
            ["acf"] = 7,
            ["abcdefg"] = 8,
            ["abcdfg"] = 9
        };

        var regex = new Regex(@"^([a-g ]+) \| ([a-g ]+)$");
        foreach (var match in reader.MatchLineByLine(regex))
        {
            var inputs = match.Groups[1].Value;
            var outputs = match.Groups[2].Value;
            yield return CreateMapping(inputs.Split(' '), outputs.Split(' '));
        }

        DecodedLine CreateMapping(string[] inputs, string[] outputs)
        {
            var result = new Dictionary<string, int>();
            var allValuesAsNumbers = inputs.Concat(outputs).ToArray();
            var possibilitiesForchars = new Dictionary<char, SevenSegmentSide>(Enumerable.Range('a', 7).Select(i => new KeyValuePair<char, SevenSegmentSide>((char)i, SevenSegmentSide.All)));

            foreach (var item in allValuesAsNumbers.Where(subStr => subStr.Length is 2 or 3 or 4))
            {
                switch (item.Length)
                {
                    case 2:
                        SetPossibilities(item, SevenSegmentSide.BottomRight | SevenSegmentSide.TopRight);
                        break;
                    case 3:
                        SetPossibilities(item, SevenSegmentSide.BottomRight | SevenSegmentSide.TopRight | SevenSegmentSide.Top);
                        break;
                    case 4:
                        SetPossibilities(item, SevenSegmentSide.TopLeft | SevenSegmentSide.TopRight | SevenSegmentSide.Middle | SevenSegmentSide.BottomRight);
                        break;
                }
            }

            // we should now have 3 entries which are equivalent to either BottomRight | TopRight or BottomRight | TopRight | Top. The one which is Top, is definetely Top
            ExcludeFromResult(SevenSegmentSide.Right, 2);
            ExcludeFromResult(SevenSegmentSide.TopLeft | SevenSegmentSide.Middle, 2);
            ExcludeFromResult(SevenSegmentSide.Top, 1);

            var charsPerSide = new Dictionary<char, SevenSegmentSide>();
            if (!CharPerSides(new(possibilitiesForchars), charsPerSide, out var decodedLine))
            {
                throw new InvalidOperationException("Failed to find chars per side");
            }
            return decodedLine;

            bool CharPerSides(Stack<KeyValuePair<char, SevenSegmentSide>> remainingValues, Dictionary<char, SevenSegmentSide> currentValues, out DecodedLine decodedLine)
            {
                if (remainingValues.Count == 0)
                {
                    return IsValid(currentValues, out decodedLine);
                }

                var currentValue = remainingValues.Pop();
                foreach (var uniqueSide in uniqueSides)
                {
                    if ((uniqueSide & currentValue.Value) == uniqueSide && !currentValues.Any(c => c.Value == uniqueSide))
                    {
                        currentValues.Add(currentValue.Key, uniqueSide);
                        if (CharPerSides(remainingValues, currentValues, out decodedLine))
                        {
                            return true;
                        }
                        currentValues.Remove(currentValue.Key);
                    }
                }
                remainingValues.Push(currentValue);
                decodedLine = null!;
                return false;
            }

            return null;

            bool IsValid(Dictionary<char, SevenSegmentSide> pairs, out DecodedLine decodedline)
            {
                decodedline = null!;
                var parsedInputs = new int[inputs.Length];
                for (var i = 0; i < inputs.Length; i++)
                {
                    var input = inputs[i];
                    if (!TryParse(input, out var parsedInput))
                    {
                        return false;
                    }
                    parsedInputs[i] = parsedInput;
                }
                var parsedOutputs = new int[outputs.Length];
                for (var i = 0; i < outputs.Length; i++)
                {
                    var output = outputs[i];
                    if (!TryParse(output, out var parsedOutput))
                    {
                        return false;
                    }
                    parsedOutputs[i] = parsedOutput;

                }
                decodedline = new()
                {
                    InputValues = parsedInputs,
                    OutputValues = parsedOutputs
                };
                return true;

                bool TryParse(string val, out int parsed)
                {
                    var side = SevenSegmentSide.None;
                    foreach (var c in val)
                    {
                        side |= pairs[c];
                    }
                    for (var i = 0; i < 10; i++)
                    {
                        if (side == sidesPerNumber[i])
                        {
                            parsed = i;
                            return true;
                        }
                    }
                    parsed = -1;
                    return false;
                }
            }
;

            void ExcludeFromResult(SevenSegmentSide sides, int expectedAmount)
            {
                var letterWhichAreRight = possibilitiesForchars.Where(possibility => possibility.Value == sides).ToArray();
                if (letterWhichAreRight.Length != expectedAmount)
                {
                    throw new ArgumentException("Failed assumption in algorithm");
                }

                foreach (var letter in possibilitiesForchars.Keys.Where(key => letterWhichAreRight.All(l => l.Key != key)).ToArray())
                {
                    possibilitiesForchars[letter] = possibilitiesForchars[letter] & ~sides;
                }
            }

            void SetPossibilities(IEnumerable<char> chars, SevenSegmentSide sides)
            {
                foreach (var c in chars)
                {
                    possibilitiesForchars[c] = possibilitiesForchars[c] & sides;
                }
            }
        }

        int[] Decode(string completeString) => completeString.Split(' ')
            .Select(number => number switch
            {
                { Length: 2 } => 1,
                { Length: 4 } => 4,
                { Length: 7 } => 8,
                { Length: 3 } => 7,
                _ => -1
            })
            .Where(number => number != -1)
            .ToArray();
    }

    private readonly SevenSegmentSide[] uniqueSides = new[] { SevenSegmentSide.TopLeft, SevenSegmentSide.Top, SevenSegmentSide.TopRight, SevenSegmentSide.Middle, SevenSegmentSide.BottomLeft, SevenSegmentSide.Bottom, SevenSegmentSide.BottomRight };
    private readonly SevenSegmentSide[] sidesPerNumber = new[] { SevenSegmentSide.Zero, SevenSegmentSide.One, SevenSegmentSide.Two, SevenSegmentSide.Three, SevenSegmentSide.Four, SevenSegmentSide.Five, SevenSegmentSide.Six, SevenSegmentSide.Seven, SevenSegmentSide.Eight, SevenSegmentSide.Nine };

    [Flags]
    private enum SevenSegmentSide
    {
        None = 0,
        TopLeft = 0b0000001,
        Top = 0b0000010,
        TopRight = 0b0000100,
        Middle = 0b0001000,
        BottomLeft = 0b0010000,
        Bottom = 0b0100000,
        BottomRight = 0b1000000,

        All = 0b1111111,
        Right = TopRight | BottomRight,

        Zero = All & ~Middle,
        One = Right,
        Two = Top | TopRight | Middle | BottomLeft | Bottom,
        Three = Top | TopRight | Middle | BottomRight | Bottom,
        Four = TopLeft | TopRight | Middle | BottomRight,
        Five = Top | TopLeft | Middle | BottomRight | Bottom,
        Six = All & ~TopRight,
        Seven = Top | TopRight | BottomRight,
        Eight = All,
        Nine = All & ~BottomLeft
    };

    private class DecodedLine
    {
        public int[] InputValues { get; init; }
        public int[] OutputValues { get; init; }
    }
}
