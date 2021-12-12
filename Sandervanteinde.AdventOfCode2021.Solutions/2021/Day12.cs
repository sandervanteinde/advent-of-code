using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day12 : BaseSolution
{
    public Day12()
        : base("Passage Pathing", 2021, 12)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var graph = ReadAsGraph(reader);
        var start = graph.GetOrCreateNode(new Cave("start"));

        return RecursiveVisitEndNode(start, new() { start });
        static int RecursiveVisitEndNode(Node<Cave> node, HashSet<Node<Cave>> visited)
        {
            var sum = 0;
            foreach (var edge in node.Edges)
            {
                var opposite = edge.OppositeOf(node);
                if (opposite.Value.Id == "end")
                {
                    sum++;
                    continue;
                }
                if (opposite.Value.IsSmallCave && visited.Contains(opposite))
                {
                    continue;
                }
                var didAdd = visited.Add(opposite);
                sum += RecursiveVisitEndNode(opposite, visited);
                if (didAdd)
                {
                    visited.Remove(opposite);
                }
            }
            return sum;
        }
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var graph = ReadAsGraph(reader);
        var start = graph.GetOrCreateNode(new Cave("start"));

        var result = RecursiveVisitEndNode(start, new() { { start, 1 } });
        return result;

        static int RecursiveVisitEndNode(Node<Cave> node, Dictionary<Node<Cave>, int> visited, bool hasVisitedSmallCaveTwice = false)
        {
            var sum = 0;
            foreach (var edge in node.Edges)
            {
                var opposite = edge.OppositeOf(node);
                if (opposite.Value.Id == "end")
                {
                    sum++;
                    continue;
                }
                var exists = visited.TryGetValue(opposite, out var visitedCount);
                if (opposite.Value.IsStartOrEnd && exists)
                {
                    continue;
                }
                if (
                    opposite.Value.IsSmallCave && (
                        (!hasVisitedSmallCaveTwice && visitedCount >= 2)
                        || (hasVisitedSmallCaveTwice && visitedCount >= 1)
                    )
                )
                {
                    continue;
                }
                visited[opposite] = visitedCount + 1;
                sum += RecursiveVisitEndNode(opposite, visited, hasVisitedSmallCaveTwice || (opposite.Value.IsSmallCave && visitedCount == 1));
                visited[opposite] = visitedCount;
            }
            return sum;
        }
    }

    private static Graph<Cave> ReadAsGraph(FileReader reader)
    {
        var graph = new Graph<Cave>();
        var regex = new Regex(@"([a-zA-Z]+)-([a-zA-Z]+)");
        foreach (var match in reader.MatchLineByLine(regex))
        {
            graph.AddEdge(new Cave(match.Groups[1].Value), new Cave(match.Groups[2].Value), 1);
        }
        return graph;
    }
}
