using System.Text.RegularExpressions;
using Sandervanteinde.AdventOfCode.Solutions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;
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
        var input = ParsePuzzleInput(reader);
        var conversions = input.Conversions.ToList();
        var rand = new Random();
        var target = input.Text;
        var mutations = 0;

        while (target != "e")
        {
            var tmp = target;
            foreach (var conversion in conversions)
            {
                var index = target.IndexOf(conversion.To);
                if (index >= 0)
                {
                    target = $"{target[..index]}{conversion.From}{target[(conversion.To.Length + index)..]}";
                    mutations++;
                }
            }

            if (tmp == target)
            {
                target = input.Text;
                mutations = 0;
                Shuffle();
            }
        }
        return mutations;

        void Shuffle()
        {
            for (var i = 0; i < 100; i++)
            {
                var i1 = rand.Next(conversions.Count);
                var i2 = rand.Next(conversions.Count);
                if (i1 == i2)
                {
                    continue;
                }

                var tmp = conversions[i1];
                conversions[i1] = conversions[i2];
                conversions[i2] = tmp;
            }
        }
    }

    private static PuzzleInput ParsePuzzleInput(FileReader reader)
    {
        string? puzzleInput = null;
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
            Text = puzzleInput ?? throw new InvalidOperationException("Expected line to be set")
        };
    }
}
