using System.Text;
using System.Text.RegularExpressions;
using Sandervanteinde.AdventOfCode.Solutions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day14 : BaseSolution
{
    public Day14()
        : base("Extended Polymerization", 2021, 14)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var (start, rules) = ParseInput(reader);
        var text = start;
        for (var i = 0; i < 10; i++)
        {
            var sb = new StringBuilder();
            for (var j = 1; j < text.Length; j++)
            {
                var pair = $"{text[j - 1]}{text[j]}";
                sb.Append(text[j - 1]);
                if (rules.TryGetValue(pair, out var rule))
                {
                    sb.Append(rule);
                }
            }
            sb.Append(text[^1]);
            text = sb.ToString();
        }

        var countByChar = text
            .GroupBy(c => c)
            .Select(grouping => new { Char = grouping.Key, Count = grouping.Count() })
            .OrderBy(grouping => grouping.Count)
            .ToList();

        var first = countByChar.First();
        var second = countByChar.Last();

        return second.Count - first.Count;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var (start, rules) = ParseInput(reader);
        var text = start;

        var results = new Dictionary<char, ulong>();
        var lookup = new Dictionary<LookupKey, ulong>();
        foreach (var c in text)
        {
            results.TryGetValue(c, out var currentCount);
            results[c] = currentCount + 1;
        }

        var cache = new Dictionary<LookupKey, Dictionary<char, ulong>>();
        for (var j = 1; j < text.Length; j++)
        {
            var pair = $"{text[j - 1]}{text[j]}";
            var lookupKey = new LookupKey
            {
                Key = $"{text[j - 1]}{text[j]}",
                Index = 1
            };
            var thingsToAdd = AddCountsToDictionary(rules, cache, in lookupKey, 40);
            foreach (var inner in thingsToAdd)
            {
                results.TryGetValue(inner.Key, out var value);
                results[inner.Key] = value + inner.Value;
            }
        }

        var countByChar = results
            .OrderBy(grouping => grouping.Value)
            .ToList();

        var first = countByChar.First();
        var second = countByChar.Last();

        return second.Value - first.Value;
    }

    public Dictionary<char, ulong> AddCountsToDictionary(IReadOnlyDictionary<string, char> rules, Dictionary<LookupKey, Dictionary<char, ulong>> lookup, in LookupKey key, int maxIndex)
    {
        if (lookup.TryGetValue(key, out var currentCount))
        {
            return currentCount;
        }

        var currentAddedChar = rules[key.Key];
        var currentAdded = new[] { new KeyValuePair<char, ulong>(rules[key.Key], 1) };
        var resultForIndex = new Dictionary<char, ulong>(currentAdded);
        lookup.Add(key, resultForIndex);
        if (maxIndex > key.Index)
        {
            var leftKey = new LookupKey { Index = key.Index + 1, Key = $"{key.Key[0]}{currentAddedChar}" };
            var rightKey = new LookupKey { Index = key.Index + 1, Key = $"{currentAddedChar}{key.Key[1]}" };
            foreach (var inner in AddCountsToDictionary(rules, lookup, in leftKey, maxIndex).Concat(AddCountsToDictionary(rules, lookup, in rightKey, maxIndex)))
            {
                resultForIndex.TryGetValue(inner.Key, out var value);
                resultForIndex[inner.Key] = value + inner.Value;
            }
        }
        return resultForIndex;

    }

    private static (string start, IReadOnlyDictionary<string, char> rules) ParseInput(FileReader reader)
    {
        var start = string.Empty;
        var fromToDictionary = new Dictionary<string, char>();
        foreach (var line in reader.ReadLineByLine())
        {
            var regex = new Regex(@"([A-Z]+) -> ([A-Z]+)");
            var match = regex.Match(line);
            if (!match.Success)
            {
                if (start != string.Empty)
                {
                    throw new InvalidOperationException("Expected only 1 start value");
                }
                start = line;
                continue;
            }

            fromToDictionary.Add(match.Groups[1].Value, match.Groups[2].Value[0]);
        }

        return (start, fromToDictionary);
    }
}
