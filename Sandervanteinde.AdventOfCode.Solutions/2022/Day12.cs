using Sandervanteinde.AdventOfCode.Solutions.Extensions;

namespace Sandervanteinde.AdventOfCode.Solutions._2022;

internal class Day12 : BaseSolution
{
    public Day12()
        : base("Hill Climbing Algorithm", 2022, 12)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var grid = reader.ReadAsGrid(c => c);

        var startWindow = grid.EnumerateEntries().First(c => c.Value == 'S');
        startWindow.Tag = 0;

        var queue = new LinkedList<GridWindow<char>>();

        foreach (var startAdjacent in startWindow.AdjacentWindows())
        {
            if (startAdjacent.Value == 'a')
            {
                startAdjacent.Tag = 1;
                queue.AddLast(startAdjacent);
            }
        }

        while (queue.Count > 0)
        {
            var window = queue.First.Value;
            queue.RemoveFirst();
            var currentValue = (int)window.Tag!;
            foreach (var adjacent in window.AdjacentWindows())
            {
                if (adjacent.Tag is not null && (int)adjacent.Tag <= (currentValue + 1))
                {
                    continue;
                }
                var diff = adjacent.Value - window.Value;
                if ((adjacent.Value == 'E' && window.Value == 'z') || (adjacent.Value != 'E' && diff is <= 1))
                {
                    adjacent.Tag = currentValue + 1;
                    Add(adjacent);
                }
            }
        }


        return grid.EnumerateEntries()
            .First(c => c.Value == 'E')
            .Tag;

        void Add(GridWindow<char> window)
        {
            if (queue.Count <= 1)
            {
                queue.AddLast(window);
                return;
            }
            var node = queue.Last;
            while ((int)node.Value.Tag > (int)window.Tag)
            {
                var prev = node;
                node = node.Previous;
                if (node is null)
                {
                    node = prev;
                    break;
                }
            }
            queue.AddAfter(node, window);
        }
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var grid = reader.ReadAsGrid(c => c);

        var startWindow = grid.EnumerateEntries().First(c => c.Value == 'S');
        startWindow.Tag = 0;

        var queue = new LinkedList<GridWindow<char>>();

        foreach (var window in grid.EnumerateEntries().Where(c => c.Value == 'a'))
        {
            window.Tag = 0;
            queue.AddLast(window);
        }

        while (queue.Count > 0)
        {
            var window = queue.First.Value;
            queue.RemoveFirst();
            var currentValue = (int)window.Tag!;
            foreach (var adjacent in window.AdjacentWindows())
            {
                if (adjacent.Tag is not null && (int)adjacent.Tag <= (currentValue + 1))
                {
                    continue;
                }
                var diff = adjacent.Value - window.Value;
                if ((adjacent.Value == 'E' && window.Value == 'z') || (adjacent.Value != 'E' && diff is <= 1))
                {
                    adjacent.Tag = currentValue + 1;
                    Add(adjacent);
                }
            }
        }


        return grid.EnumerateEntries()
            .First(c => c.Value == 'E')
            .Tag;

        void Add(GridWindow<char> window)
        {
            if (queue.Count <= 1)
            {
                queue.AddLast(window);
                return;
            }
            var node = queue.Last;
            while ((int)node.Value.Tag > (int)window.Tag)
            {
                var prev = node;
                node = node.Previous;
                if (node is null)
                {
                    node = prev;
                    break;
                }
            }
            queue.AddAfter(node, window);
        }
    }
}
