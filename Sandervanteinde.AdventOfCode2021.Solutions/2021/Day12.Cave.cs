namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day12
{
    private class Cave : IEquatable<Cave>
    {
        public string Id { get; }
        public bool IsSmallCave { get; }
        public bool IsStartOrEnd { get; }
        public Cave(string id)
        {
            Id = id;
            IsSmallCave = id.ToLower() == id;
            IsStartOrEnd = id is "start" or "end";
        }


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
