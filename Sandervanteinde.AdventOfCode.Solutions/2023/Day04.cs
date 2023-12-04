using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2023;

internal partial class Day04 : BaseSolution
{
    public Day04()
        : base("Scratchcards", year: 2023, day: 4)
    {
    }

    [GeneratedRegex(@"Card +(\d+): ([\d ]+) \| ([\d ]+)")]
    public static partial Regex LineRegex();

    public override object DetermineStepOneResult(FileReader reader)
    {
        var parseFn = ParseCard;
        var sum = 0L;
        foreach (var card in reader.MatchLineByLine(LineRegex(), parseFn))
        {
            sum += (long)Math.Pow(2, card.WinCount - 1);
        }

        return sum;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var parseFn = ParseCard;
        var cards = reader.MatchLineByLine(LineRegex(), parseFn)
            .ToList();

        var cardCounts = cards.ToDictionary(
            c => c.Id, _ => 1L
        );
        for (var i = 0; i < cards.Count; i++)
        {
            if (cardCounts[i] == 0)
            {
                continue;
            }

            var amount = cardCounts[i];
            var winAmount = cards[i].WinCount;

            for (var j = 1; j <= winAmount; j++)
            {
                cardCounts[i + j] += amount;
            }
        }

        return cardCounts.Sum(c => c.Value);
    }

    private static Card ParseCard(int id, string winningCardsInput, string ownCardsInput)
    {
        var winningCards = winningCardsInput.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToHashSet();
        
        var ownCards = ownCardsInput.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToHashSet();
        
        ownCards.IntersectWith(winningCards);

        var card = new Card { Id = id - 1, WinCount = ownCards.Count };
        return card;
    }

    public class Card
    {
        public required int Id { get; init; }
        public int WinCount { get; init; }
    }
}
