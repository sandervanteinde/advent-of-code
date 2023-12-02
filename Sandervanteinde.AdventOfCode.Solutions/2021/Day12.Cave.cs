namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day12
{
    private class Cave : IEquatable<Cave>
    {
        public Cave(string id)
        {
            Id = id;
            IsSmallCave = id.ToLower() == id;
            IsStartOrEnd = id is "start" or "end";
        }

        public string Id { get; }
        public bool IsSmallCave { get; }
        public bool IsStartOrEnd { get; }

        public bool Equals(Cave? other)
        {
            return Id.Equals(other?.Id);
        }

        public override string ToString()
        {
            return Id;
        }
    }
}
