namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal class Day21 : BaseSolution
{
    public Day21()
        : base(@"Dirac Dice", year: 2021, day: 21)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var (playerOne, playerTwo) = DetermineStartingPositions(reader);
        var playerOneScore = 0L;
        var playerTwoScore = 0L;
        var die = new DeterministicDie();

        while (playerOneScore < 1000 || playerTwoScore < 1000)
        {
            var playerOneMoves = die.Roll(rollCount: 3);
            playerOne = BindToBoard(playerOne + playerOneMoves);

            playerOneScore += playerOne;

            if (playerOneScore >= 1000)
            {
                break;
            }

            var playerTwoMoves = die.Roll(rollCount: 3);
            playerTwo = BindToBoard(playerTwo + playerTwoMoves);

            while (playerTwo > 10)
            {
                playerTwo -= 10;
            }

            playerTwoScore += playerTwo;
        }

        var losingPlayerCount = playerOneScore > playerTwoScore
            ? playerTwoScore
            : playerTwoScore;
        return losingPlayerCount * die.RollCount;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var (playerOne, playerTwo) = DetermineStartingPositions(reader);
        var winsForRollValue = new Dictionary<int, int>();

        foreach (var (dieOne, dieTwo, dieThree) in AllDice())
        {
            var totalValue = dieOne + dieTwo + dieThree;
            winsForRollValue.TryGetValue(totalValue, out var currentValue);
            winsForRollValue[totalValue] = ++currentValue;
        }

        var cache = new Dictionary<GameState, (long playerOneWins, long playerTwoWins)>();
        var (playerOneWins, playerTwoWins) = CalculateWins(new GameState(PlayerTurn: 0, playerOne, playerTwo));
        return Math.Max(playerOneWins, playerTwoWins);

        (long playerOneWins, long playerTwoWins) CalculateWins(
            in GameState gameState
        )
        {
            if (gameState.PlayerOneScore >= 21)
            {
                cache[gameState] = (1, 0);
            }

            if (gameState.PlayerTwoScore >= 21)
            {
                cache[gameState] = (0, 1);
            }

            if (cache.TryGetValue(gameState, out var cachedValue))
            {
                return cachedValue;
            }

            var playerOneWins = 0L;
            var playerTwoWins = 0L;

            foreach (var (roll, rollValue) in winsForRollValue)
            {
                if (gameState.PlayerTurn == 0) // player one
                {
                    var newPosition = BindToBoard(gameState.PlayerOnePosition + roll);
                    var (winsP1, winsP2) = CalculateWins(
                        gameState with { PlayerOneScore = gameState.PlayerOneScore + newPosition, PlayerOnePosition = newPosition, PlayerTurn = 1 }
                    );
                    playerOneWins += winsP1 * rollValue;
                    playerTwoWins += winsP2 * rollValue;
                    continue;
                }

                if (gameState.PlayerTurn == 1)
                {
                    var newPosition = BindToBoard(gameState.PlayerTwoPosition + roll);
                    var (winsP1, winsP2) = CalculateWins(
                        gameState with { PlayerTwoScore = gameState.PlayerTwoScore + newPosition, PlayerTwoPosition = newPosition, PlayerTurn = 0 }
                    );
                    playerOneWins += winsP1 * rollValue;
                    playerTwoWins += winsP2 * rollValue;
                }
            }

            cache[gameState] = (playerOneWins, playerTwoWins);
            return (playerOneWins, playerTwoWins);
        }
    }

    private static IEnumerable<(int dieOne, int dieTwo, int dieThree)> AllDice()
    {
        for (var i = 1; i <= 3; i++)
        {
            for (var j = 1; j <= 3; j++)
            {
                for (var k = 1; k <= 3; k++)
                {
                    yield return (i, j, k);
                }
            }
        }
    }

    private int BindToBoard(int value)
    {
        while (value > 10)
        {
            value -= 10;
        }

        return value;
    }

    private (int playerOne, int playerTwo) DetermineStartingPositions(FileReader reader)
    {
        var lines = reader.ReadLineByLine();
        return (lines.First()
            .Last() - 48, lines.Last()
            .Last() - 48);
    }

    public readonly record struct GameState(int PlayerTurn, int PlayerOnePosition, int PlayerTwoPosition, int PlayerOneScore = 0, int PlayerTwoScore = 0);

    private class DeterministicDie
    {
        private int _currentValue;
        public int RollCount { get; private set; }

        public int Roll(int rollCount)
        {
            var sum = 0;

            for (var i = 0; i < rollCount; i++)
            {
                RollCount++;
                ++_currentValue;

                if (_currentValue == 101)
                {
                    _currentValue = 1;
                }

                sum += _currentValue;
            }

            return sum;
        }
    }
}
