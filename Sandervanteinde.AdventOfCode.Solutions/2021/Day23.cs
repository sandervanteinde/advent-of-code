namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day23 : BaseSolution
{
    public Day23()
        : base(@"Amphipod", 2021, 23)
    {

    }

    public const char EMPTY = '.';

    public override object DetermineStepOneResult(FileReader reader)
    {
        return -1;
        var board = ParseGameBoard(reader);
        var lowestScore = long.MaxValue;
        var visited = new Dictionary<string, long>();

        var iterateCount = 0L;
        PerformStep(board, 0);
        return lowestScore;

        void PerformStep(char[] board, long score)
        {
            if (score >= lowestScore)
            {
                return;
            }
            var id = GameBoard.StringIdentifier(board);
            if (visited.TryGetValue(id, out var existingScore) && existingScore <= score)
            {
                return;
            }
            visited[id] = score;
            if (++iterateCount % 1000 == 0)
            {
            }

            if (GameBoard.IsCorrect(board))
            {
                lowestScore = Math.Min(lowestScore, score);
                return;
            }

            var availableSteps = GameBoard.AllOptions(board);
            foreach (var (newBoard, newScore) in availableSteps)
            {
                PerformStep(newBoard, score + newScore);
            }
        }
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var board = ParseLargeGameBoard(reader);
        var lowestScore = long.MaxValue;
        var visited = new Dictionary<string, long>();

        var iterateCount = 0L;
        PerformStep(board, 0);
        return lowestScore;

        void PerformStep(char[] board, long score)
        {
            if (score >= lowestScore)
            {
                return;
            }
            var id = GameBoardLarge.StringIdentifier(board);
            if (visited.TryGetValue(id, out var existingScore) && existingScore <= score)
            {
                return;
            }
            visited[id] = score;
            if (++iterateCount % 1000 == 0)
            {
            }

            if (GameBoardLarge.IsCorrect(board))
            {
                if (score == 41089L)
                {
                    ;
                }
                lowestScore = score;
                return;
            }

            var availableSteps = GameBoardLarge.AllOptions(board);
            foreach (var (newBoard, newScore) in availableSteps)
            {
                PerformStep(newBoard, score + newScore);
            }
        }
    }

    public static char[] ParseGameBoard(FileReader reader)
    {
        var lines = reader.ReadLineByLine().ToArray();
        return new char[]
        {
            lines[1][1],
            lines[1][2],
            lines[1][4],
            lines[1][6],
            lines[1][8],
            lines[1][10],
            lines[1][11],
            lines[2][3],
            lines[3][3],
            lines[2][5],
            lines[3][5],
            lines[2][7],
            lines[3][7],
            lines[2][9],
            lines[3][9]
        };
    }

    public static char[] ParseLargeGameBoard(FileReader reader)
    {
        var lines = reader.ReadLineByLine().ToArray();
        return new char[]
        {
            lines[1][1],
            lines[1][2],
            lines[1][4],
            lines[1][6],
            lines[1][8],
            lines[1][10],
            lines[1][11],
            lines[2][3],
            'D',
            'D',
            lines[3][3],
            lines[2][5],
            'C',
            'B',
            lines[3][5],
            lines[2][7],
            'B',
            'A',
            lines[3][7],
            lines[2][9],
            'A',
            'C',
            lines[3][9]
        };
    }
}