using System.Text.Json;
using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions.Utils;

public class FileReader
{
    public string Input { get; }

    public FileReader(string input)
    {
        Input = input;
    }

    public IEnumerable<string> ReadLineByLine(StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
    {
        var test = Input.Split(new char[] { '\n', '\r' }, options);
        return test;
    }

    public void ReadAndProcessLineByLine(Func<string, CancellationTokenSource, bool> processedFn)
    {
        var items = (string[])ReadLineByLine();
        var source = new CancellationTokenSource();
        var token = source.Token;
        var nextItemList = new List<string>();

        while (items.Length > 0)
        {
            foreach (var item in items)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                if (!processedFn(item, source))
                {
                    nextItemList.Add(item);
                }
            }

            items = nextItemList.ToArray();
            nextItemList.Clear();
        }

    }

    public IEnumerable<int> ReadCommaSeperatedNumbers()
    {
        return Input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse);
    }

    public IEnumerable<Match> MatchLineByLine(Regex regex)
    {
        foreach (var line in ReadLineByLine())
        {
            var match = regex.Match(line);
            yield return match.Success
                ? match
                : throw new InvalidOperationException($"Expected each line to match regex '{regex}', but '{line}' did not match.");
        }
    }

    public GridWindow<T>[,] ReadAsGrid<T>(Func<char, T> mapper)
    {
        var lines = new List<List<T>>();
        foreach (var line in ReadLineByLine())
        {
            var parsedLine = new List<T>(line.Length);
            foreach (var c in line)
            {
                parsedLine.Add(mapper(c));
            }
            lines.Add(parsedLine);
        }

        var grid = new T[lines.First().Count, lines.Count];
        for (var y = 0; y < lines.Count; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Count; x++)
            {
                grid[x, y] = line[x];
            }
        }
        return grid.ToGridwindows();
    }

    public GridWindow<int>[,] ReadAsIntegerGrid()
    {
        return ReadAsGrid(c => c - 48);
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
