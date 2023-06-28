using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day15 : BaseSolution
{
    public Day15()
        : base("Science for Hungry People", 2015, 15)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var ingredients = ParseLines(reader).ToArray();
        var maxTotal = int.MinValue;
        foreach (var possibility in IngredientsWithAmounts(ingredients))
        {
            var capacity = Math.Max(possibility.Sum(possibility => possibility.Key.Capacity * possibility.Value), 0);
            var durability = Math.Max(possibility.Sum(possibility => possibility.Key.Durability * possibility.Value), 0);
            var flavor = Math.Max(possibility.Sum(possibility => possibility.Key.Flavor * possibility.Value), 0);
            var texture = Math.Max(possibility.Sum(possibility => possibility.Key.Texture * possibility.Value), 0);
            var total = capacity * durability * flavor * texture;

            maxTotal = Math.Max(maxTotal, total);
        }

        return maxTotal;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var ingredients = ParseLines(reader).ToArray();
        var maxTotal = int.MinValue;
        foreach (var possibility in IngredientsWithAmounts(ingredients))
        {
            var calories = Math.Max(possibility.Sum(possibility => possibility.Key.Calories * possibility.Value), 0);
            if (calories is not 500)
            {
                continue;
            }

            var capacity = Math.Max(possibility.Sum(possibility => possibility.Key.Capacity * possibility.Value), 0);
            var durability = Math.Max(possibility.Sum(possibility => possibility.Key.Durability * possibility.Value), 0);
            var flavor = Math.Max(possibility.Sum(possibility => possibility.Key.Flavor * possibility.Value), 0);
            var texture = Math.Max(possibility.Sum(possibility => possibility.Key.Texture * possibility.Value), 0);
            var total = capacity * durability * flavor * texture;

            maxTotal = Math.Max(maxTotal, total);
        }

        return maxTotal;
    }

    private IEnumerable<Dictionary<Ingredient, int>> IngredientsWithAmounts(IEnumerable<Ingredient> ingredients)
    {
        var dictionary = new Dictionary<Ingredient, int>();
        var remainig = new Stack<Ingredient>(ingredients);
        var sum = 0;

        return Iterate();

        IEnumerable<Dictionary<Ingredient, int>> Iterate()
        {
            if (remainig.Count == 0)
            {
                if (sum == 100)
                {
                    yield return dictionary;
                }
                yield break;
            }

            var currentIngredient = remainig.Pop();
            for (var i = 0; i <= 100; i++)
            {
                dictionary[currentIngredient] = i;
                sum += i;
                foreach (var inner in Iterate())
                {
                    yield return inner;
                }
                sum -= i;
            }
            remainig.Push(currentIngredient);
        }
    }

    private IEnumerable<int> EnumerateZeroToHundred()
    {
        for (var i = 0; i <= 100; i++)
        {
            yield return i;
        }
    }

    private static IEnumerable<Ingredient> ParseLines(FileReader reader)
    {
        var regex = new Regex(@"([a-zA-Z]+): capacity (-?\d+), durability (-?\d+), flavor (-?\d+), texture (-?\d+), calories (-?\d+)");
        foreach (var match in reader.MatchLineByLine(regex))
        {
            var product = match.Groups[1].Value;
            var capacity = int.Parse(match.Groups[2].Value);
            var durability = int.Parse(match.Groups[3].Value);
            var flavor = int.Parse(match.Groups[4].Value);
            var texture = int.Parse(match.Groups[5].Value);
            var calories = int.Parse(match.Groups[6].Value);

            yield return new Ingredient
            {
                Product = product,
                Capacity = capacity,
                Durability = durability,
                Flavor = flavor,
                Texture = texture,
                Calories = calories
            };
        }
    }
}
