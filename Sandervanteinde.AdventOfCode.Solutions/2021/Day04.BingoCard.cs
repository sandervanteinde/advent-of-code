namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day04
{
    private class BingoCard
    {
        public BingoCard(int[][] rows, int[][] columns)
        {
            Rows = rows.Select(
                    row => row.Select(item => new BingoItem(item))
                        .ToArray()
                )
                .ToArray();
            Columns = columns.Select(
                    column => column.Select(item => new BingoItem(item))
                        .ToArray()
                )
                .ToArray();
            Locations = new Dictionary<int, Point>();

            for (var x = 0; x < rows.Length; x++)
            {
                var row = rows[x];

                for (var y = 0; y < row.Length; y++)
                {
                    var item = row[y];
                    Locations.Add(item, new Point { X = x, Y = y });
                }
            }
        }

        public Dictionary<int, Point> Locations { get; }
        public BingoItem[][] Rows { get; }
        public BingoItem[][] Columns { get; }

        public bool HasBingoAfterMarking(int item)
        {
            if (!Locations.TryGetValue(item, out var point))
            {
                return false;
            }

            Rows[point.X][point.Y].Tagged = true;
            Columns[point.Y][point.X].Tagged = true;
            return HasBingo(point);
        }

        private bool HasBingo(Point itemTagged)
        {
            return Rows[itemTagged.X]
                    .All(item => item.Tagged)
                || Columns[itemTagged.Y]
                    .All(item => item.Tagged);
        }

        public int SumOfUnmarkedItems()
        {
            return Rows
                .SelectMany(row => row)
                .Where(item => !item.Tagged)
                .Sum(item => item.Value);
        }
    }
}
