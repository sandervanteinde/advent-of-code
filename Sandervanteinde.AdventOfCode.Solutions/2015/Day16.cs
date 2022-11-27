using System.Text.RegularExpressions;
using Sandervanteinde.AdventOfCode.Solutions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day16 : BaseSolution
{
    public Day16()
        : base("Aunt Sue", 2015, 16)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var expectedPossessions = new[]
        {
            new Possession { Name = "children", Amount = 3 },
            new Possession { Name = "cats", Amount = 7 },
            new Possession { Name = "samoyeds", Amount = 2 },
            new Possession { Name = "pomeranians", Amount = 3 },
            new Possession { Name = "akitas", Amount = 0 },
            new Possession { Name = "vizslas", Amount = 0 },
            new Possession { Name = "goldfish", Amount = 5 },
            new Possession { Name = "trees", Amount = 3 },
            new Possession { Name = "cars", Amount = 2 },
            new Possession { Name = "perfumes", Amount = 1 }
        }.ToDictionary(x => x.Name, x => x.Amount);

        return ParseSues(reader).First(IsMatch).Number;

        bool IsMatch(Sue sue) => sue.Possessions.All(possession => expectedPossessions[possession.Name] == possession.Amount);
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var expectedPossessions = new[]
        {
            new PossessionValidator { Name = "children", Amount = value => value == 3 },
            new PossessionValidator { Name = "cats", Amount = value => value > 7 },
            new PossessionValidator { Name = "samoyeds", Amount = value => value == 2 },
            new PossessionValidator { Name = "pomeranians", Amount = value => value < 3 },
            new PossessionValidator { Name = "akitas", Amount = value => value == 0 },
            new PossessionValidator { Name = "vizslas", Amount = value => value == 0 },
            new PossessionValidator { Name = "goldfish", Amount = value => value < 5 },
            new PossessionValidator { Name = "trees", Amount = value => value > 3 },
            new PossessionValidator { Name = "cars", Amount = value => value == 2 },
            new PossessionValidator { Name = "perfumes", Amount = value => value == 1 }
        }.ToDictionary(x => x.Name, x => x.Amount);

        return ParseSues(reader).First(IsMatch).Number;

        bool IsMatch(Sue sue) => sue.Possessions.All(possession => expectedPossessions[possession.Name](possession.Amount));
    }

    private static IEnumerable<Sue> ParseSues(FileReader reader)
    {
        var regex = new Regex(@"Sue (\d+): (.+)");
        foreach (var line in reader.MatchLineByLine(regex))
        {
            var possessions = line.Groups[2].Value.Split(", ");
            yield return new Sue
            {
                Number = int.Parse(line.Groups[1].Value),
                Possessions = possessions
                    .Select(x =>
                    {
                        var split = x.Split(": ");
                        return new Possession
                        {
                            Name = split[0],
                            Amount = int.Parse(split[1])
                        };
                    })
                    .ToList()
                    .AsReadOnly()
            };
        }
    }
}
