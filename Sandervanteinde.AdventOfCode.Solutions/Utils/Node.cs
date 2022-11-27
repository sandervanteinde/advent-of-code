namespace Sandervanteinde.AdventOfCode.Solutions.Utils;

internal record Node<T>(T Value)
{
    public List<Edge<T>> Edges { get; } = new();

    public override string ToString()
    {
        return Value?.ToString();
    }
}

