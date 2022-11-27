using Sandervanteinde.AdventOfCode.Solutions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day21 : BaseSolution
{
    public Day21()
        : base("RPG Simulator 20XX", 2015, 21)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var (hitpoints, damage, armor) = ParseDragonStats(reader);
        var (weapons, armors, rings) = GetStoreItems();
        var allItems = weapons
            .Concat<Item>(armors)
            .Concat(rings)
            .OrderBy(item => item.Cost)
            .ToList();

        var possibilities = EnumeratePossibilities(weapons, armors, rings)
            .OrderBy(possibility => possibility.cost);

        foreach (var (cost, items) in possibilities)
        {
            if (IsDragonSlain(items, hitpoints, damage, armor))
            {
                return cost;
            }
        }

        throw new InvalidOperationException("A result could not be determined");
    }

    private static bool IsDragonSlain(IEnumerable<Item> items, int hitpoints, int damage, int armor)
    {
        var playerDamage = items.Sum(item => item.Damage);
        var playerArmor = items.Sum(item => item.Armor);
        var dragonHealth = hitpoints;
        var playerHealth = 100;
        var playerTurn = true;
        while (playerHealth > 0 && dragonHealth > 0)
        {
            if (playerTurn)
            {
                dragonHealth -= Math.Max(0, (playerDamage - armor));
            }
            else
            {
                playerHealth -= Math.Max(0, (damage - playerArmor));
            }
            playerTurn = !playerTurn;
        }
        return dragonHealth < 0;

    }

    private IEnumerable<(int cost, IEnumerable<Item> items)> EnumeratePossibilities(IList<Weapon> weapons, IList<ArmorItem> armors, IList<Ring> rings)
    {
        var result = new LinkedList<(int cost, IEnumerable<Item> items)>();
        var totalCost = 0;
        var items = new List<Item>();
        foreach (var weapon in weapons)
        {
            items.Add(weapon);
            totalCost += weapon.Cost;
            Add();
            // with armor
            foreach (var armor in armors)
            {
                items.Add(armor);
                totalCost += armor.Cost;
                // 0 rings
                Add();

                // 1 ring
                IterateRings();

                totalCost -= armor.Cost;
                RemoveLastItem();
            }

            // with only rings
            IterateRings();

            RemoveLastItem();
            totalCost -= weapon.Cost;
        }

        return result;

        void RemoveLastItem() => items.RemoveAt(items.Count - 1);

        void Add() => result.AddLast((totalCost, items.ToArray()));

        void IterateRings()
        {
            for (var i = 0; i < rings.Count; i++)
            {
                var ring = rings[i];
                totalCost += ring.Cost;
                items.Add(rings[i]);
                Add();
                for (var j = i + 1; j < rings.Count; j++)
                {
                    var secondRing = rings[j];
                    totalCost += secondRing.Cost;
                    items.Add(secondRing);
                    Add();
                    totalCost -= secondRing.Cost;
                    RemoveLastItem();
                }
                totalCost -= ring.Cost;
                RemoveLastItem();
            }
        }
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var (hitpoints, damage, armor) = ParseDragonStats(reader);
        var (weapons, armors, rings) = GetStoreItems();
        var allItems = weapons
            .Concat<Item>(armors)
            .Concat(rings)
            .OrderBy(item => item.Cost)
            .ToList();

        var possibilities = EnumeratePossibilities(weapons, armors, rings)
            .OrderByDescending(possibility => possibility.cost);

        foreach (var (cost, items) in possibilities)
        {
            if (!IsDragonSlain(items, hitpoints, damage, armor))
            {
                return cost;
            }
        }

        throw new InvalidOperationException("A result could not be determined");
    }

    private static (IList<Weapon> weapons, IList<ArmorItem> armors, IList<Ring> rings) GetStoreItems()
    {
        return (Weapons().ToArray(), Armors().ToArray(), Rings().ToArray());

        IEnumerable<Weapon> Weapons()
        {
            yield return new()
            {
                Name = "Dagger",
                Damage = 4,
                Cost = 8
            };
            yield return new()
            {
                Name = "Shortsword",
                Cost = 10,
                Damage = 5
            };
            yield return new()
            {
                Name = "Warhammer",
                Cost = 25,
                Damage = 6
            };
            yield return new()
            {
                Name = "Longsword",
                Cost = 40,
                Damage = 7
            };
            yield return new()
            {
                Name = "Greataxe",
                Cost = 74,
                Damage = 8
            };
        }

        IEnumerable<ArmorItem> Armors()
        {
            yield return new()
            {
                Name = "Leather",
                Cost = 13,
                Armor = 1
            };
            yield return new()
            {
                Name = "Chainmail",
                Cost = 31,
                Armor = 2
            };
            yield return new()
            {
                Name = "Splintmail",
                Cost = 53,
                Armor = 3
            };
            yield return new()
            {
                Name = "Bandedmail",
                Cost = 75,
                Armor = 4
            };
            yield return new()
            {
                Name = "Platemail",
                Cost = 102,
                Armor = 5
            };
        }

        IEnumerable<Ring> Rings()
        {
            yield return new()
            {
                Name = "Damage +1",
                Cost = 25,
                Damage = 1
            };
            yield return new()
            {
                Name = "Damage +2",
                Cost = 50,
                Damage = 2
            };
            yield return new()
            {
                Name = "Damage +3",
                Cost = 100,
                Damage = 3
            };

            yield return new()
            {
                Name = "Defense +1",
                Cost = 20,
                Armor = 1
            };
            yield return new()
            {
                Name = "Defense +2",
                Cost = 40,
                Armor = 2
            };
            yield return new()
            {
                Name = "Defense +3",
                Cost = 80,
                Armor = 3
            };
        }
    }

    private static (int hitpoints, int damage, int armor) ParseDragonStats(FileReader reader)
    {
        int hitpoints = -1, damage = -1, armor = -1;
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
                case 'A':
                    armor = value;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
        return (hitpoints, damage, armor);
    }
}
