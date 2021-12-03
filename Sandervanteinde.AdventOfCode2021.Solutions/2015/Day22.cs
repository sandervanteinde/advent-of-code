namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day22 : BaseSolution
{
    private enum LogLevel
    {
        Success,
        SuccessAndFail,
        None
    }
    private const LogLevel logLevel = LogLevel.Success;
    public Day22()
        : base("Wizard Simulator 20XX", 2015, 22)
    {

    }

    private record struct GameStats
    {
        public int PlayerHealth { get; set; }
        public int PlayerArmor { get; set; }
        public int PlayerMana { get; set; }

        public int DragonHealth { get; set; }
        public int DragonDamage { get; set; }
    }

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

    public override object DetermineStepOneResult(FileReader reader)
    {
        var (hitPoints, damage) = ParseDragonStats(reader);
        var allSpells = Spell.All().OrderBy(spell => spell.Mana).ToArray();
        var spells = new Stack<Spell>();
        var lowestManaKill = int.MaxValue;
        var initGameStats = new GameStats
        {
            DragonDamage = damage,
            DragonHealth = hitPoints,
            PlayerArmor = 0,
            PlayerHealth = 50,
            PlayerMana = 500
        };
        var castSpells = new Stack<Spell>();
        AttemptCastSpell(initGameStats, Enumerable.Empty<Effect>());

        return lowestManaKill;

        void AttemptCastSpell(GameStats startStats, IEnumerable<Effect> startEffects, int turn = 1)
        {
            if (lowestManaKill < castSpells.Sum(s => s.Mana))
            {
                return;
            }
            foreach (var spell in allSpells)
            {
                var stats = startStats;
                IEnumerable<Effect> effects = startEffects.ToArray();
                // can player cast this spell
                // player turn effects
                stats = ApplyEffects(stats, ref effects);

                if (ShouldStop(ref stats))
                {
                    continue;
                }

                if (spell.Mana > stats.PlayerMana || !spell.CanCastSpell(effects))
                {
                    continue;
                }

                // player turn cast
                CastAndReturn(spell, () =>
                {
                    stats = spell.CastSpell(stats, ref effects);

                    if (ShouldStop(ref stats))
                    {
                        return;
                    }

                    // dragon turn effects
                    stats = ApplyEffects(stats, ref effects);
                    if (ShouldStop(ref stats))
                    {
                        return;
                    }

                    // dragon attack
                    stats = DragonAttack(stats);
                    if (ShouldStop(ref stats))
                    {
                        return;
                    }
                    AttemptCastSpell(stats, effects, turn + 1);
                });

                void CastAndReturn(Spell spell, Action act)
                {
                    castSpells.Push(spell);
                    act();
                    castSpells.Pop();
                }
            }


        }

        GameStats ApplyEffects(GameStats stats, ref IEnumerable<Effect> spellEffects)
        {
            spellEffects = spellEffects.Select(effect => effect with { Turn = effect.Turn - 1 });
            foreach (var effect in spellEffects)
            {
                stats = effect.ApplyEffect(stats);
            }
            return stats;
        }

        bool ShouldStop(ref GameStats stats)
        {
            if (stats.PlayerHealth <= 0 || stats.DragonHealth <= 0)
            {
                var playerWon = stats.PlayerHealth > 0;
                var manaSpent = castSpells.Sum(spell => spell.Mana);
                if (playerWon)
                {
                    lowestManaKill = Math.Min(lowestManaKill, castSpells.Sum(spell => spell.Mana));
                }
                LogReplay(playerWon, manaSpent);
                if (playerWon)
                {
                    Console.WriteLine(stats);
                }

                return true;
            }
            return false;
        }

        GameStats DragonAttack(GameStats stats) => stats with
        {
            PlayerHealth = stats.PlayerHealth - Math.Max(1, stats.DragonDamage - stats.PlayerArmor)
        };

        void LogReplay(bool won, int manaSpent)
        {
            switch (logLevel)
            {
                case LogLevel.Success:
                    if (!won)
                    {
                        return;
                    }

                    break;
                case LogLevel.None:
                    return;
            }
            var gameStats = initGameStats;
            var effects = Enumerable.Empty<Effect>();
            Console.ForegroundColor = won ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"Player {(won ? "won" : "lost")}");
            Console.ForegroundColor = ConsoleColor.Gray;
            foreach (var spell in castSpells.Reverse())
            {
                gameStats = ApplyEffects(gameStats, ref effects);
                if (Log("Apply pre-player effects"))
                {
                    break;
                }

                gameStats = spell.CastSpell(gameStats, ref effects);
                if (Log(spell.GetType().Name))
                {
                    break;
                }

                gameStats = ApplyEffects(gameStats, ref effects);
                if (Log("Apply pre-dragon effects"))
                {
                    break;
                }

                gameStats = DragonAttack(gameStats);
                if (Log("Dragon attack"))
                {
                    break;
                }
            }
            Console.WriteLine(string.Join(" -> ", castSpells.Reverse().Select(spell => spell.GetType().Name)));
            Console.WriteLine($"Mana spent: {manaSpent}");
            Console.WriteLine();

            bool Log(string text)
            {
                Console.WriteLine(text);
                Console.WriteLine(gameStats);
                Console.WriteLine();
                return gameStats.PlayerHealth <= 0 || gameStats.DragonHealth <= 0;
            }
        }
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        throw new NotImplementedException();
    }
    private static (int hitpoints, int damage) ParseDragonStats(FileReader reader)
    {
        int hitpoints = -1, damage = -1;
        foreach (var line in reader.ReadLineByLine())
        {
            var value = int.Parse(line.Split(": ")[1]);
            switch (line[0])
            {
                case 'H':
                    hitpoints = value;
                    break;
                case 'D':
                    damage = value;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
        return (hitpoints, damage);
    }
}
