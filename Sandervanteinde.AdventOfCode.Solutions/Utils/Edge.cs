namespace Sandervanteinde.AdventOfCode.Solutions.Utils;

internal record Edge<T>(Node<T> Left, Node<T> Right, int Distance)
{
    public Node<T> OppositeOf(Node<T> node)
    {
        if (!ContainsNode(node))
        {
            throw new InvalidOperationException();
        }

        return node == Left
            ? Right
            : Left;
    }

    public bool ContainsNode(Node<T> node)
    {
        return node == Left || node == Right;
    }

    public override string ToString()
    {
        return $"{Left} <=> {Right} (Distance: {Distance})";
    }
}
