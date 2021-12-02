namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal class Day22 : BaseSolution
{
    public Day22()
        : base("Wizard Simulator 20XX", 2015, 22)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        return "NYI";
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        return "NYI";
    }

    //public override object DetermineStepOneResult(FileReader reader)
    //{
    //    var (hitPoints, damage) = ParseDragonStats(reader);
    //    var ctx = new FightContext { DragonHealth = hitPoints, DragonDamage = damage, PlayerHealth = 50, PlayerMana = 500 };
    //    var playerTurn = true;
    //    var allSpells = Spell.All().ToArray();
    //    var spells = new Stack<Spell>();
    //    var lowestManaKill = int.MaxValue;

    //    for (var i = 0; i < allSpells.Length; i++)
    //    {
    //        // player turn
    //        ctx.PerformEffects();
    //        var spell = allSpells[i];
    //        spells.Push(spell);
    //        spell.Cast(ctx);
    //        if (ShouldStop())
    //        {
    //            break;
    //        }

    //        // dragon turn
    //        ctx.PerformEffects();

    //    }

    //    while (ctx.PlayerHealth > 0 && ctx.DragonHealth > 0)
    //    {
    //        if (playerTurn)
    //        {
    //        }
    //        else
    //        {
    //            ctx.PlayerHealth -= Math.Max(1, ctx.DragonDamage - ctx.PlayerArmor);
    //        }

    //        playerTurn = !playerTurn;
    //    }

    //    return null;

    //    bool ShouldStop()
    //    {
    //        if (ctx.PlayerHealth <= 0 || ctx.DragonHealth <= 0)
    //        {
    //            if (ctx.DragonHealth <= 0)
    //            {
    //                lowestManaKill = Math.Min(lowestManaKill, spells.Sum(spell => spell.Mana));
    //            }
    //            return true;
    //        }
    //        return false;
    //    }
    //}

    //public override object DetermineStepTwoResult(FileReader reader)
    //{
    //    throw new NotImplementedException();
    //}
    //private static (int hitpoints, int damage) ParseDragonStats(FileReader reader)
    //{
    //    int hitpoints = -1, damage = -1;
    //    foreach (var line in reader.ReadLineByLine())
    //    {
    //        var value = int.Parse(line.Split(": ")[1]);
    //        switch (line[0])
    //        {
    //            case 'H':
    //                hitpoints = value;
    //                break;
    //            case 'D':
    //                damage = value;
    //                break;
    //            default:
    //                throw new InvalidOperationException();
    //        }
    //    }
    //    return (hitpoints, damage);
    //}

    //private abstract class Spell
    //{
    //    public abstract int Mana { get; }

    //    public static IEnumerable<Spell> All()
    //    {
    //        yield return new MagicMissile();
    //        yield return new Drain();
    //        yield return new Shield();
    //        yield return new Poison();
    //        yield return new Recharge();
    //    }
    //}
    //private class MagicMissile : Spell
    //{
    //    public override int Mana => 53;

    //    public override void Cast(FightContext ctx)
    //    {
    //        ctx.DragonHealth -= 4;
    //    }

    //    public override void Undo(FightContext ctx)
    //    {
    //        ctx.DragonHealth += 4;
    //    }
    //}
    //private class Drain : Spell
    //{
    //    public override int Mana => 73;

    //    public override void Cast(FightContext ctx)
    //    {
    //        ctx.DragonHealth -= 2;
    //        ctx.PlayerHealth += 2;
    //    }

    //    public override void Undo(FightContext ctx)
    //    {
    //        ctx.DragonHealth += 2;
    //        ctx.PlayerHealth -= 2;
    //    }
    //}

    //private class Shield : Spell
    //{
    //    public override int Mana => 113;

    //    public override void Cast(FightContext ctx)
    //    {
    //        ctx.PlayerArmor += 7;
    //        ctx.AddEffect((ctx, turn) =>
    //        {
    //            if (turn == 0)
    //            {
    //                ctx.PlayerArmor -= 7;
    //            }
    //        }, 6);
    //    }

    //    public override void Undo(FightContext ctx)
    //    {
    //        ctx.PlayerArmor -= 7;
    //        ctx.RemoveLastAppliedEffect();
    //    }
    //}

    //private class Poison : Spell
    //{
    //    public override int Mana => 173;

    //    public override void Cast(FightContext ctx)
    //    {
    //        ctx.AddEffect((ctx, turn) =>
    //        {
    //            ctx.DragonHealth -= 3;
    //        }, 6);
    //    }

    //    public override void Undo(FightContext ctx)
    //    {
    //        ctx.RemoveLastAppliedEffect();
    //    }
    //}

    //private class Recharge : Spell
    //{
    //    public override int Mana => 229;

    //    public override void Cast(FightContext ctx)
    //    {
    //        ctx.AddEffect((ctx, turn) =>
    //        {
    //            ctx.PlayerMana += 101;
    //        }, 5);
    //    }
    //}
}
