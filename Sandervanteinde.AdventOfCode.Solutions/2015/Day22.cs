namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day22 : BaseSolution
{
    private readonly LogLevel logLevel = LogLevel.Success;
    private int lowestManaKill;

    public Day22()
        : base("Wizard Simulator 20XX", year: 2015, day: 22)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var (hitPoints, damage) = ParseDragonStats(reader);
        var allSpells = Spell.All()
            .OrderBy(spell => spell.Mana)
            .ToArray();
        var spells = new Stack<Spell>();
        var initGameStats = new GameStats
        {
            DragonDamage = damage,
            DragonHealth = hitPoints,
            PlayerArmor = 0,
            PlayerHealth = 50,
            PlayerMana = 500
        };
        var castSpells = new Stack<Spell>();
        lowestManaKill = int.MaxValue;
        AttemptCastSpell(initGameStats, initGameStats, Enumerable.Empty<Effect>(), turn: 1, allSpells, castSpells);

        return lowestManaKill;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var (hitPoints, damage) = ParseDragonStats(reader);
        var allSpells = Spell.All()
            .OrderBy(spell => spell.Mana)
            .ToArray();
        var spells = new Stack<Spell>();
        var initGameStats = new GameStats
        {
            DragonDamage = damage,
            DragonHealth = hitPoints,
            PlayerArmor = 0,
            PlayerHealth = 50,
            PlayerMana = 500,
            LoseOneHpAtStart = true
        };
        var castSpells = new Stack<Spell>();
        lowestManaKill = int.MaxValue;
        AttemptCastSpell(initGameStats, initGameStats, Enumerable.Empty<Effect>(), turn: 1, allSpells, castSpells);

        return lowestManaKill;
    }

    private static GameStats DragonAttack(GameStats stats)
    {
        return stats with { PlayerHealth = stats.PlayerHealth - Math.Max(val1: 1, stats.DragonDamage - stats.PlayerArmor) };
    }

    private void LogReplay(bool won, int manaSpent, GameStats initGameStats, Stack<Spell> castSpells)
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
        Console.ForegroundColor = won
            ? ConsoleColor.Green
            : ConsoleColor.Red;
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

            if (Log(
                    spell.GetType()
                        .Name
                ))
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

        Console.WriteLine(
            string.Join(
                " -> ", castSpells.Reverse()
                    .Select(
                        spell => spell.GetType()
                            .Name
                    )
            )
        );
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

    private bool ShouldStop(ref GameStats stats, Stack<Spell> castSpells, GameStats initGameState)
    {
        if (stats.PlayerHealth <= 0 || stats.DragonHealth <= 0)
        {
            var playerWon = stats.PlayerHealth > 0;
            var manaSpent = castSpells.Sum(spell => spell.Mana);

            if (playerWon)
            {
                lowestManaKill = Math.Min(lowestManaKill, castSpells.Sum(spell => spell.Mana));
            }

            LogReplay(playerWon, manaSpent, initGameState, castSpells);

            if (playerWon)
            {
                Console.WriteLine(stats);
            }

            return true;
        }

        return false;
    }

    private static GameStats ApplyEffects(GameStats stats, ref IEnumerable<Effect> spellEffects)
    {
        spellEffects = spellEffects.Select(effect => effect with { Turn = effect.Turn - 1 });

        foreach (var effect in spellEffects)
        {
            stats = effect.ApplyEffect(stats);
        }

        return stats;
    }

    private void AttemptCastSpell(GameStats initGameState, GameStats startStats, IEnumerable<Effect> startEffects, int turn, Spell[] allSpells,
        Stack<Spell> castSpells)
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
            stats = ApplyEffects(stats, ref effects) with
            {
                PlayerHealth = initGameState.LoseOneHpAtStart
                    ? stats.PlayerHealth - 1
                    : stats.PlayerHealth
            };

            if (ShouldStop(ref stats, castSpells, initGameState))
            {
                continue;
            }

            if (spell.Mana >= stats.PlayerMana || !spell.CanCastSpell(effects))
            {
                continue;
            }

            // player turn cast
            CastAndReturn(
                spell, () =>
                {
                    stats = spell.CastSpell(stats, ref effects);

                    if (ShouldStop(ref stats, castSpells, initGameState))
                    {
                        return;
                    }

                    // dragon turn effects
                    stats = ApplyEffects(stats, ref effects);

                    if (ShouldStop(ref stats, castSpells, initGameState))
                    {
                        return;
                    }

                    // dragon attack
                    stats = DragonAttack(stats);

                    if (ShouldStop(ref stats, castSpells, initGameState))
                    {
                        return;
                    }

                    AttemptCastSpell(initGameState, stats, effects, turn + 1, allSpells, castSpells);
                }
            );

            void CastAndReturn(Spell spell, Action act)
            {
                castSpells.Push(spell);
                act();
                castSpells.Pop();
            }
        }
    }

    private static (int hitpoints, int damage) ParseDragonStats(FileReader reader)
    {
        int hitpoints = -1, damage = -1;

        foreach (var line in reader.ReadLineByLine())
        {
            var value = int.Parse(line.Split(": ")[1]);

            switch (line[index: 0])
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

    private enum LogLevel
    {
        Success,
        SuccessAndFail,
        None
    }
}
