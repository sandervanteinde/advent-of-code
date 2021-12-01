using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day19 : BaseSolution
{
    public Day19()
        : base("Medicine for Rudolph", 2015, 19)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var input = ParsePuzzleInput(reader);
        var uniqueMolecules = new HashSet<string>();
        foreach (var replacement in input.Conversions)
        {
            var regex = new Regex(replacement.From);
            var matchIndex = input.Text.IndexOf(replacement.From, 0);
            while (matchIndex >= 0)
            {
                var newMolecule = regex.Replace(input.Text, replacement.To, 1, matchIndex);
                uniqueMolecules.Add(newMolecule);
                matchIndex = input.Text.IndexOf(replacement.From, matchIndex + 1);
            }
            input.Text.IndexOf(replacement.From);

        }

        return uniqueMolecules.Count;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        return "Not solved :-(";
        var input = ParsePuzzleInput(reader);
        var currentMolecule = input.Text;
        var replacements = 0;
        var visited = new HashSet<string>();

        return AttemptDecipher(currentMolecule, 0, visited)!.Value;

        while (currentMolecule != "e")
        {
            foreach (var conversion in input.Conversions)
            {
                var index = currentMolecule.IndexOf(conversion.To);
                if (index >= 0)
                {
                    var regex = new Regex(conversion.To);
                    currentMolecule = regex.Replace(currentMolecule, conversion.From, 1, index);
                    replacements++;
                    break;
                }
            }
        }
        return 0;

        int? AttemptDecipher(string original, int count, HashSet<string> attempted)
        {
            foreach (var conversion in input.Conversions)
            {
                var index = original.IndexOf(conversion.To);
                while (index >= 0)
                {
                    var regex = new Regex(conversion.To);
                    var newModule = regex.Replace(original, conversion.From, 1, index);
                    if (newModule == "e")
                    {
                        return count + 1;
                    }
                    if (attempted.Add(newModule))
                    {
                        var amount = AttemptDecipher(newModule, count + 1, attempted);
                        if (amount is not null)
                        {
                            return amount;
                        }
                    }

                    index = original.IndexOf(conversion.To, index + 1);
                }
            }
            return null;
        }

    }

    private static PuzzleInput ParsePuzzleInput(FileReader reader)
    {

        string puzzleInput = null;
        var conversions = new LinkedList<Conversion>();
        var regex = new Regex(@"([a-zA-Z]+) => ([A-Za-z]+)");
        foreach (var line in reader.ReadLineByLine())
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            var match = regex.Match(line);
            if (match.Success)
            {
                conversions.AddLast(new Conversion { From = match.Groups[1].Value, To = match.Groups[2].Value });
            }
            else if (puzzleInput is null)
            {
                puzzleInput = line;
            }
            else
            {
                throw new InvalidOperationException("Invalid parsed puzzle input");
            }
        }

        return new PuzzleInput
        {
            Conversions = conversions,
            Text = puzzleInput
        };
    }
}
