namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day15
{
    private class Ingredient
    {
        public string Product { get; init; } = null!;
        public int Capacity { get; init; }
        public int Durability { get; init; }
        public int Flavor { get; init; }
        public int Texture { get; init; }
        public int Calories { get; init; }
    }
}
