namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day22
{
    private abstract class Spell
    {
        public abstract int Mana { get; }

        public GameStats CastSpell(GameStats stats, ref IEnumerable<Effect> effects)
        {
            return OnCast(stats, ref effects) with
            {
                PlayerMana = stats.PlayerMana - Mana
            };
        }
        protected abstract GameStats OnCast(GameStats stats, ref IEnumerable<Effect> effects);

        public static IEnumerable<Spell> All()
        {
            yield return new Shield();
            yield return new Poison();
            yield return new Recharge();
            yield return new MagicMissile();
            yield return new Drain();
        }

        public virtual bool CanCastSpell(IEnumerable<Effect> effects)
        {
            return true;
        }
    }
}
