namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day22
{
    private class Shield : Spell
    {
        public override int Mana => 113;

        protected override GameStats OnCast(GameStats stats, ref IEnumerable<Effect> effects)
        {
            effects = effects.Concat(new Effect[] { new(ShieldEffect, 6) });
            return stats with
            {
                PlayerArmor = stats.PlayerArmor + 7
            };
        }

        private static GameStats ShieldEffect(GameStats state, int turn)
        {
            return turn switch
            {
                0 => state with { PlayerArmor = state.PlayerArmor - 7 },
                _ => state
            };
        }

        public override bool CanCastSpell(IEnumerable<Effect> effects)
        {
            foreach (var effect in effects)
            {
                if (effect.Apply == ShieldEffect && effect.Turn > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
