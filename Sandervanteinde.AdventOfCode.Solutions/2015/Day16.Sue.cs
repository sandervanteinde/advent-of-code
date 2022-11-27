namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day16
{
    public class Sue
    {
        public int Number { get; init; }
        public IReadOnlyCollection<Possession> Possessions { get; init; } = Array.Empty<Possession>();
    }
}
