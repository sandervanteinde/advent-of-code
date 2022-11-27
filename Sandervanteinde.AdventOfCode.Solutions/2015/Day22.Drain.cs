namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day22
{
    private class Drain : Spell
    {
        public override int Mana => 73;

        protected override GameStats OnCast(GameStats stats, ref IEnumerable<Effect> effects)
        {
            if (Log)
            {
                Console.WriteLine("Draining 2 health from dragon");
            }
            return stats with
            {
                DragonHealth = stats.DragonHealth - 2,
                PlayerHealth = stats.PlayerHealth + 2
            };
        }
    }
}
