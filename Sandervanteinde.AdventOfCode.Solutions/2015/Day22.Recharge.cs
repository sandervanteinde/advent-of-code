﻿namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day22
{
    private class Recharge : Spell
    {
        public override int Mana => 229;

        protected override GameStats OnCast(GameStats stats, ref IEnumerable<Effect> effects)
        {
            effects = effects.Concat(new Effect[] { new(RechargeEffect, turns: 5) });
            return stats;
        }

        private static GameStats RechargeEffect(GameStats stats, int turn)
        {
            return turn switch
            {
                >= 0 => stats with { PlayerMana = stats.PlayerMana + 101 },
                _ => stats
            };
        }

        public override bool CanCastSpell(IEnumerable<Effect> effects)
        {
            foreach (var effect in effects)
            {
                if (effect.Apply == RechargeEffect && effect.Turn > 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
