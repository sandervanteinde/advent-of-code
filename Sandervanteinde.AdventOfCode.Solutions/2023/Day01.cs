using System.Buffers;

namespace Sandervanteinde.AdventOfCode.Solutions._2023;

internal class Day01 : BaseSolution
{
    public Day01()
        : base("Trebuchet?!", year: 2023, day: 1)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var sum = 0L;
        foreach (var line in reader.ReadAsSpanLineByLine())
        {
            var first = line.IndexOfAnyInRange('0', '9');
            var last = line.LastIndexOfAnyInRange('0', '9');
            var number = (line[first] - 48) * 10 + (line[last] - 48);
            sum += number;
        }

        return sum;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var searchValues = SearchValues.Create("1,2,3,4,5,6,7,8,9,one,two,three,four,five,six,seven,eight,nine,zero");
        var values = new Dictionary<string, int>
        {
            ["1"] = 1,
            ["2"] = 2,
            ["3"] = 3,
            ["4"] = 4,
            ["5"] = 5,
            ["6"] = 6,
            ["7"] = 7,
            ["8"] = 8,
            ["9"] = 9,
            ["0"] = 0,
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4,
            ["five"] = 5,
            ["six"] = 6,
            ["seven"] = 7,
            ["eight"] = 8,
            ["nine"] = 9
        };
        var indices = new List<(int Index, string Key)>(values.Count);
        var lastIndices = new List<(int Index, string Key)>(values.Count);
        var sum = 0;
        foreach (var line in reader.ReadAsSpanLineByLine())
        {
            foreach (var key in values.Keys)
            {
                var index = line.IndexOf(key);
                var lastIndex = line.LastIndexOf(key);

                if (index >= 0)
                {
                    indices.Add((index, key));
                }

                if (lastIndex >= 0)
                {
                    lastIndices.Add((lastIndex, key));
                }
            }

            var first = indices.MinBy(c => c.Index)
                .Key;
            var last = lastIndices.MaxBy(c => c.Index)
                .Key;

            var value = values[first] * 10 + values[last];
            sum += value;

            indices.Clear();
            lastIndices.Clear();
        }

        return sum;
    }
}

