using System.Text.Json;
using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Utils;

internal class FileReader
{
    public string Input { get; }

    public FileReader(string input)
    {
        Input = input;
    }

    public IEnumerable<string> ReadLineByLine()
    {
        var test = Input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return test;
    }

    public IEnumerable<Match> MatchLineByLine(Regex regex)
    {
        foreach (var line in ReadLineByLine())
        {
            var match = regex.Match(line);
            if (!match.Success)
            {
                throw new InvalidOperationException($"Expected each line to match regex '{regex}', but '{line}' did not match.");
            }
            yield return match;
        }
    }

    public IEnumerable<char> ReadCharByChar()
    {
        return Input;
    }

    public T? DeserializeJsonAs<T>()
    {
        return JsonSerializer.Deserialize<T>(Input);
    }
}
