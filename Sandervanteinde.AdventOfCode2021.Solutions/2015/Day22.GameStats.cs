namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day22
{
    private record struct GameStats
    {
        public int PlayerHealth { get; set; }
        public int PlayerArmor { get; set; }
        public int PlayerMana { get; set; }

        public int DragonHealth { get; set; }
        public int DragonDamage { get; set; }

        public bool LoseOneHpAtStart { get; set; }
    }
}
