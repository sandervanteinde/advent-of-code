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
            if (!match.Success)
            {
                throw new InvalidOperationException($"Expected each line to match regex '{regex}', but '{line}' did not match.");
            }
            yield return match;
        }
    }

    public T[,] ReadAsGrid<T>(Func<char, T> mapper)
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
        return grid;
    }

    public GridWindow<int>[,] ReadAsIntegerGrid()
    {
        var integerGrid = ReadAsGrid(c => c - 48);
        var xLength = integerGrid.GetLength(0);
        var yLength = integerGrid.GetLength(1);
        var grid = new GridWindow<int>[xLength, yLength];
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                grid[x, y] = new GridWindow<int>(integerGrid, x, y);
            }
        }
        return grid;
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
