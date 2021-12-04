namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day22
{
    private struct Effect
    {
        public int Turn { get; set; }
        public Func<GameStats, int, GameStats> Apply { get; }

        public Effect(Func<GameStats, int, GameStats> apply, int turns)
        {
            Apply = apply;
            Turn = turns;
        }
        public GameStats ApplyEffect(GameStats gameStats)
        {
            return Apply(gameStats, Turn);
        }
    }
}
