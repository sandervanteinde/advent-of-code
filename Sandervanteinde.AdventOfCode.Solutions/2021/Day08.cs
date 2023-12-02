using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day08 : BaseSolution
{
    /// <summary>
    ///     The sides representing the number on which the index of that side resides.
    /// </summary>
    private readonly SevenSegmentSide[] sidesPerNumber =
    {
        SevenSegmentSide.Zero, SevenSegmentSide.One, SevenSegmentSide.Two, SevenSegmentSide.Three, SevenSegmentSide.Four, SevenSegmentSide.Five,
        SevenSegmentSide.Six, SevenSegmentSide.Seven, SevenSegmentSide.Eight, SevenSegmentSide.Nine
    };

    /// <summary>
    ///     All flags which represent a single side
    /// </summary>
    private readonly SevenSegmentSide[] uniqueSides =
    {
        SevenSegmentSide.TopLeft, SevenSegmentSide.Top, SevenSegmentSide.TopRight, SevenSegmentSide.Middle, SevenSegmentSide.BottomLeft,
        SevenSegmentSide.Bottom, SevenSegmentSide.BottomRight
    };

    public Day08()
        : base("Seven Segment Search", year: 2021, day: 8)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var outputsWith147or8 = 0;

        foreach (var line in DecodeLines(reader))
        {
            outputsWith147or8 += line.OutputValues.Where(i => i is 1 or 4 or 7 or 8)
                .Count();
        }

        return outputsWith147or8;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var totalSum = 0;

        foreach (var line in DecodeLines(reader))
        {
            totalSum += (line.OutputValues[0] * 1000) + (line.OutputValues[1] * 100) + (line.OutputValues[2] * 10) + line.OutputValues[3];
        }

        return totalSum;
    }

    private IEnumerable<DecodedLine> DecodeLines(FileReader reader)
    {
        var regex = new Regex(@"^([a-g ]+) \| ([a-g ]+)$");

        foreach (var match in reader.MatchLineByLine(regex))
        {
            var inputs = match.Groups[groupnum: 1].Value;
            var outputs = match.Groups[groupnum: 2].Value;
            yield return CreateDecodedLine(inputs.Split(separator: ' '), outputs.Split(separator: ' '));
        }

        DecodedLine CreateDecodedLine(string[] inputs, string[] outputs)
        {
            var allValuesAsNumbers = inputs.Concat(outputs)
                .ToArray();
            var possibilitiesForchars = new Dictionary<char, SevenSegmentSide>(
                Enumerable.Range(start: 'a', count: 7)
                    .Select(i => new KeyValuePair<char, SevenSegmentSide>((char)i, SevenSegmentSide.All))
            );

            // From these values we know which sides are correct.
            foreach (var item in allValuesAsNumbers.Where(subStr => subStr.Length is 2 or 3 or 4))
            {
                switch (item.Length)
                {
                    case 2:
                        SetPossibilities(item, SevenSegmentSide.One);
                        break;
                    case 3:
                        SetPossibilities(item, SevenSegmentSide.Seven);
                        break;
                    case 4:
                        SetPossibilities(item, SevenSegmentSide.Four);
                        break;
                    // eight is irrelevant, it has all sides and will not clear anything.
                }
            }

            // we should now have 3 entries which are equivalent to either BottomRight | TopRight or BottomRight | TopRight | Top. The one which is Top, is definetely Top
            ExcludeFromResult(SevenSegmentSide.Right, expectedAmount: 2);
            ExcludeFromResult(SevenSegmentSide.TopLeft | SevenSegmentSide.Middle, expectedAmount: 2);
            ExcludeFromResult(SevenSegmentSide.Top, expectedAmount: 1);

            var charsPerSide = new Dictionary<char, SevenSegmentSide>();

            if (!DetermineDecodedLine(new Stack<KeyValuePair<char, SevenSegmentSide>>(possibilitiesForchars), charsPerSide, out var decodedLine))
            {
                throw new InvalidOperationException("Failed to find chars per side");
            }

            return decodedLine;

            bool DetermineDecodedLine(Stack<KeyValuePair<char, SevenSegmentSide>> remainingValues, Dictionary<char, SevenSegmentSide> currentValues,
                out DecodedLine decodedLine)
            {
                // if all values are determined to their respective side, verify if it fits with our input
                if (remainingValues.Count == 0)
                {
                    return IsValid(currentValues, out decodedLine);
                }

                // take the next value that needs to be assessed
                var currentValue = remainingValues.Pop();

                foreach (var uniqueSide in uniqueSides)
                {
                    // Can this value fit in one of the unique sides and is that side unused
                    if ((uniqueSide & currentValue.Value) == uniqueSide && !currentValues.Any(c => c.Value == uniqueSide))
                    {
                        // lets try that
                        currentValues.Add(currentValue.Key, uniqueSide);

                        // And set the next value
                        if (DetermineDecodedLine(remainingValues, currentValues, out decodedLine))
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

            // Verifies if the given mapping will work for the provided input
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

                decodedline = new DecodedLine { InputValues = parsedInputs, OutputValues = parsedOutputs };
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
                var letterWhichAreRight = possibilitiesForchars.Where(possibility => possibility.Value == sides)
                    .ToArray();

                if (letterWhichAreRight.Length != expectedAmount)
                {
                    throw new ArgumentException("Failed assumption in algorithm");
                }

                foreach (var letter in possibilitiesForchars.Keys.Where(key => letterWhichAreRight.All(l => l.Key != key))
                             .ToArray())
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
    }
}
