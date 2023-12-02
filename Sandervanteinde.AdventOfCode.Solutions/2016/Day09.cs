using System.Text;

namespace Sandervanteinde.AdventOfCode.Solutions._2016;

internal class Day09 : BaseSolution
{
    public Day09()
        : base("Explosives in Cyberspace", year: 2016, day: 9)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var sb = new StringBuilder();
        var partialText = string.Empty;
        var subsequentChars = 0;
        var characterReader = reader.ReadCharByChar()
            .GetEnumerator();

        while (characterReader.MoveNext())
        {
            var c = characterReader.Current;

            switch (c)
            {
                case ' ': continue;
                case '(':
                    partialText = string.Empty;
                    break;
                case 'x':
                    subsequentChars = int.Parse(partialText);
                    partialText = string.Empty;
                    break;
                case >= '0' and <= '9':
                    partialText += c;
                    break;
                case ')':
                    var amount = int.Parse(partialText);
                    var subsequentString = new StringBuilder();

                    for (var i = 0; i < subsequentChars; i++)
                    {
                        characterReader.MoveNext();
                        subsequentString.Append(characterReader.Current);
                    }

                    for (var i = 0; i < amount; i++)
                    {
                        sb.Append(subsequentString);
                    }

                    break;
                default:
                    sb.Append(c);
                    break;
            }
        }

        return sb.Length;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        return GetDecodedLengthVersion2(reader.Input);
    }

    internal ulong GetDecodedLengthVersion2(ReadOnlySpan<char> text)
    {
        var firstIndexOfBrace = text.IndexOf(value: '(');

        if (firstIndexOfBrace == -1)
        {
            return (ulong)text.Length;
        }

        var remainingText = text[firstIndexOfBrace..];
        var xLocation = remainingText.IndexOf(value: 'x');
        var endBraceIndex = remainingText.IndexOf(value: ')');
        var subsequentChars = int.Parse(remainingText[1..xLocation]);
        var amount = ulong.Parse(remainingText[(xLocation + 1)..endBraceIndex]);
        var repeatedSection = remainingText[(endBraceIndex + 1)..(endBraceIndex + 1 + subsequentChars)];
        var afterRepeatedSection = remainingText[(endBraceIndex + 1 + subsequentChars)..];

        return (ulong)firstIndexOfBrace +
            (GetDecodedLengthVersion2(repeatedSection) * amount)
            + GetDecodedLengthVersion2(afterRepeatedSection);
    }

    private enum State
    {
    }
}
