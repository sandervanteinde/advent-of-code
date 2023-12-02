namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day06 : BaseSolution
{
    public Day06()
        : base("Lanternfish", year: 2021, day: 6)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        return Count(reader, amount: 80);
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        return Count(reader, amount: 256);
    }

    private long Count(FileReader reader, int amount)
    {
        var initialFish = ParseToFish(reader)
            .ToArray();
        var fishMadeAtTurnWithNewFish = new Dictionary<int, long>();
        var result = initialFish.Select(fish => CalculateCreatedFishForTurn(fish.TurnMade + fish.State + 1))
            .Sum() + initialFish.Length;
        return result;

        long CalculateCreatedFishForTurn(int turn)
        {
            var fishCreated = 0L;

            if (turn > amount)
            {
                return fishCreated;
            }

            if (!fishMadeAtTurnWithNewFish.TryGetValue(turn, out fishCreated))
            {
                var newFishTurn = turn;

                while (newFishTurn <= amount)
                {
                    fishCreated += 1 + CalculateCreatedFishForTurn(newFishTurn + 9);
                    newFishTurn += 7;
                }

                fishMadeAtTurnWithNewFish[turn] = fishCreated;
            }

            return fishCreated;
        }
    }

    public static IEnumerable<LanternFish> ParseToFish(FileReader reader)
    {
        return reader.Input.Split(",")
            .Select(val => new LanternFish(int.Parse(val), turnMade: 0));
    }
}
