namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day22
{
    private class Poison : Spell
    {
        public override int Mana => 173;

        protected override GameStats OnCast(GameStats stats, ref IEnumerable<Effect> effects)
        {
            effects = effects.Concat(new Effect[] { new(PoisonEffect, 6) });
            return stats;
        }

        private static GameStats PoisonEffect(GameStats state, int turn)
        {
            return turn switch
            {
                >= 0 => state with { DragonHealth = state.DragonHealth - 3 },
                _ => state
            };
        }

        public override bool CanCastSpell(IEnumerable<Effect> effects)
        {
            foreach (var effect in effects)
            {
                if (effect.Apply == PoisonEffect && effect.Turn > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
