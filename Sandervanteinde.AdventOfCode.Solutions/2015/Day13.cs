using System.Text.RegularExpressions;
using Sandervanteinde.AdventOfCode.Solutions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day13 : BaseSolution
{
    public Day13()
        : base("Knights of the Dinner Table", 2015, 13)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        return FindBestScore(reader);
    }

    private int FindBestScore(FileReader reader, bool seatSelf = false)
    {
        const string SELF = "self";
        var pairs = GetPairRatings(reader);
        var uniquePersons = UniquePersonsIn(pairs.Keys);
        if (seatSelf)
        {
            foreach (var person in uniquePersons)
            {
                var pairWithSelf = new Pair(SELF, person);
                pairs.Add(pairWithSelf, 0);
            }
            uniquePersons.Add(SELF);
        }

        var bestScore = int.MinValue;
        int currentScore;
        string? firstPerson;
        for (var i = 0; i < uniquePersons.Count; i++)
        {
            firstPerson = uniquePersons[i];
            uniquePersons.RemoveAt(i);
            currentScore = 0;
            IteratePossibilities(firstPerson);
            uniquePersons.Insert(i, firstPerson);
        }

        return bestScore;

        void IteratePossibilities(string previousPerson)
        {
            if (uniquePersons.Count == 0)
            {
                var pairScore = pairs[new Pair(previousPerson, firstPerson)];
                currentScore += pairScore;
                if (currentScore > bestScore)
                {
                    bestScore = currentScore;
                }
                currentScore -= pairScore;
                return;
            }
            for (var j = 0; j < uniquePersons.Count; j++)
            {
                var seatWith = uniquePersons[j];
                var pairScore = pairs[new Pair(previousPerson, seatWith)];

                currentScore += pairScore;
                uniquePersons.RemoveAt(j);
                IteratePossibilities(seatWith);
                uniquePersons.Insert(j, seatWith);
                currentScore -= pairScore;
            }
        }
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        return FindBestScore(reader, seatSelf: true);
    }

    private List<string> UniquePersonsIn(IEnumerable<Pair> pairs)
    {
        return pairs
            .SelectMany(pair => new[] { pair.PersonOne, pair.PersonTwo })
            .Distinct()
            .ToList();
    }

    private Dictionary<Pair, int> GetPairRatings(FileReader reader)
    {
        var regex = new Regex(@"([A-Za-z]+) would (lose|gain) (\d+) happiness units by sitting next to ([A-Za-z]+)");
        var result = new Dictionary<Pair, int>(new Pair.Comparer());

        foreach (var match in reader.MatchLineByLine(regex))
        {
            var personOne = match.Groups[1].Value;
            var lose = match.Groups[2].Value is "lose";
            var amount = int.Parse(match.Groups[3].Value);
            var personTwo = match.Groups[4].Value;
            amount = lose ? -amount : amount;

            var pair = new Pair(personOne, personTwo);
            if (result.TryGetValue(pair, out var pairRatings))
            {
                amount = pairRatings + amount;
            }
            result[pair] = amount;
        }

        return result;
    }
}
