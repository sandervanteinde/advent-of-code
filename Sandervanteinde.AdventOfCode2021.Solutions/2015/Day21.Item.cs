namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day21
{
    private class Item
    {
        public int Cost { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Armor { get; init; }
        public int Damage { get; init; }

        public override string ToString()
        {
            return Name;
        }
    }
}
