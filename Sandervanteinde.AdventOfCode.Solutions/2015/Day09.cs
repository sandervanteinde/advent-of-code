﻿using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal class Day09 : BaseSolution
{
    public Day09()
        : base("All in a Single Night", year: 2015, day: 9)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var graph = ParseInput(reader);
        return graph.BruteForceShortestDistanceToAll();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var graph = ParseInput(reader);
        return graph.BruteForceLongestDistanceToAll();
    }

    private static Graph<string> ParseInput(FileReader reader)
    {
        var regex = new Regex(@"([A-Za-z]+) to ([A-Za-z]+) = (\d+)");
        var graph = new Graph<string>();

        foreach (var match in reader.MatchLineByLine(regex))
        {
            var from = match.Groups[groupnum: 1].Value;
            var to = match.Groups[groupnum: 2].Value;
            var distance = int.Parse(match.Groups[groupnum: 3].ValueSpan);
            graph.AddEdge(from, to, distance);
        }

        return graph;
    }
}
