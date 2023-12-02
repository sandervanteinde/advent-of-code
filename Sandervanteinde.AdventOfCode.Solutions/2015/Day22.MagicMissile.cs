namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day22
{
    private class MagicMissile : Spell
    {
        public override int Mana => 53;

        protected override GameStats OnCast(GameStats stats, ref IEnumerable<Effect> effects)
        {
            if (Log)
            {
                Console.WriteLine("Dealing 4 damage with magic missile");
            }

            return stats with { DragonHealth = stats.DragonHealth - 4 };
        }
    }
}
