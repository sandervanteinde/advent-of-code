namespace Sandervanteinde.AdventOfCode.Solutions.Utils;

internal class Graph<T>
    where T : IEquatable<T>
{
    private readonly List<Node<T>> _nodes = new();

    public void AddEdge(T left, T right, int distance)
    {
        var leftNode = GetOrCreateNode(left);
        var rightNode = GetOrCreateNode(right);
        var edge = new Edge<T>(leftNode, rightNode, distance);
        leftNode.Edges.Add(edge);
        rightNode.Edges.Add(edge);
    }

    public Node<T> GetOrCreateNode(T value)
    {
        var node = _nodes.Find(node => node.Value.Equals(value));

        if (node is null)
        {
            node = new Node<T>(value);
            _nodes.Add(node);
        }

        return node;
    }

    public int BruteForceShortestDistanceToAll()
    {
        return BruteForce(int.MaxValue, (prev, newValues) => Math.Min(prev, newValues.Min()));
    }

    public int BruteForceLongestDistanceToAll()
    {
        return BruteForce(int.MinValue, (prev, newValues) => Math.Max(prev, newValues.Max()));
    }

    private int BruteForce(int initialValue, Func<int, IEnumerable<int>, int> onNewValue)
    {
        var target = initialValue;

        foreach (var node in _nodes)
        {
            var remainingNodes = new HashSet<Node<T>>(_nodes);
            remainingNodes.Remove(node);
            var possibilities = IteratePossibilities(remainingNodes, node, distance: 0);
            target = onNewValue(target, possibilities);
        }

        return target;
    }

    private static IEnumerable<int> IteratePossibilities(HashSet<Node<T>> nodesToVisit, Node<T> currentNode, int distance)
    {
        if (nodesToVisit.Count == 0)
        {
            yield return distance;
        }

        foreach (var edge in currentNode.Edges)
        {
            if (!edge.ContainsNode(currentNode))
            {
                continue;
            }

            var otherSide = edge.OppositeOf(currentNode);

            if (!nodesToVisit.Remove(otherSide))
            {
                continue;
            }

            foreach (var possibility in IteratePossibilities(nodesToVisit, otherSide, distance + edge.Distance))
            {
                yield return possibility;
            }

            nodesToVisit.Add(otherSide);
        }
    }
}
