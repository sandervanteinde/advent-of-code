using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal class Day05 : BaseSolution
{
    public Day05()
        : base("Doesn't He Have Intern-Elves For This?", 2015, 5)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var vowels = new Regex("[aeiou]", RegexOptions.Compiled);
        var repeatedLetter = new Regex("([a-z])\\1", RegexOptions.Compiled);
        var disallowedCombinations = new[]
        {
            "ab", "cd", "pq", "xy"
        };

        var niceTexts = 0;

        foreach(var line in reader.ReadLineByLine())
        {
            if(disallowedCombinations.Any(combination => line.Contains(combination)))
            {
                continue;
            }

            var hasRepeatedLetter = repeatedLetter.IsMatch(line);
            if (!hasRepeatedLetter)
            {
                continue;
            }

            var vowelMatches = vowels.Matches(line);
            if(vowelMatches.Count < 3)
            {
                continue;
            }
            niceTexts++;
        }

        return niceTexts;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var notOverlappingTwoLetters = new Regex("([a-z])([a-z]).*\\1\\2", RegexOptions.Compiled);
        var repeated = new Regex("([a-z])[a-z]\\1", RegexOptions.Compiled);
        var niceWords = 0;
        foreach(var line in reader.ReadLineByLine())
        {
            if(notOverlappingTwoLetters.IsMatch(line) && repeated.IsMatch(line))
            {
                niceWords++;
            }
        }

        return niceWords;
    }
}
