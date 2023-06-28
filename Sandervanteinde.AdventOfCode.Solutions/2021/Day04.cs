namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day04 : BaseSolution
{
    public Day04()
        : base("Giant Squid", 2021, 4)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var (numbers, cards) = ParseInput(reader);
        foreach (var number in numbers)
        {
            foreach (var card in cards)
            {
                if (card.HasBingoAfterMarking(number))
                {
                    return number * card.SumOfUnmarkedItems();
                }
            }
        }

        throw new InvalidOperationException("No result was found");
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var (numbers, initialCards) = ParseInput(reader);
        var notWonCards = new HashSet<BingoCard>(initialCards);
        foreach (var number in numbers)
        {
            var cards = notWonCards.ToArray();
            foreach (var card in cards)
            {
                if (card.HasBingoAfterMarking(number))
                {
                    if (notWonCards.Count == 1)
                    {
                        return number * card.SumOfUnmarkedItems();
                    }
                    notWonCards.Remove(card);
                }
            }
        }

        throw new InvalidOperationException("No result was found");
    }

    private static (IEnumerable<int> numbers, IEnumerable<BingoCard> cards) ParseInput(FileReader reader)
    {
        var textReader = new StringReader(reader.Input);
        var numbers = textReader.ReadLine()!.Split(',').Select(int.Parse).ToArray();
        textReader.ReadLine();
        return (numbers, ParseBingoCards().ToArray());

        IEnumerable<BingoCard> ParseBingoCards()
        {
            var line = textReader.ReadLine();
            var columns = EmptyBingoCard();
            var rows = EmptyBingoCard();
            var i = 0;
            while (line is not null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    yield return new BingoCard(rows, columns);
                    columns = EmptyBingoCard();
                    rows = EmptyBingoCard();
                    i = 0;
                }
                else
                {
                    var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                    rows[i] = numbers;
                    for (var j = 0; j < numbers.Length; j++)
                    {
                        columns[j][i] = numbers[j];
                    }
                    i++;
                }
                line = textReader.ReadLine();
            }

            yield return new BingoCard(rows, columns);

            int[][] EmptyBingoCard() => Enumerable.Range(0, 5)
                    .Select(_ => Enumerable.Range(0, 5).ToArray())
                    .ToArray();
        }
    }
}
