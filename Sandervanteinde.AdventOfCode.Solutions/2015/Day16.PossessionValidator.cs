namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day16
{
    public class PossessionValidator
    {

        public string Name { get; init; } = string.Empty;
        public Predicate<int> Amount { get; init; } = _ => false;
    }
}
