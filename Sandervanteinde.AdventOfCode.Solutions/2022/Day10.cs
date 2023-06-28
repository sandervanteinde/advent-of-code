using System.Text;

namespace Sandervanteinde.AdventOfCode.Solutions._2022;

internal class Day10 : BaseSolution
{
    public Day10()
        : base("Cathode-Ray Tube", 2022, 10)
    {
            
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var cycle = 0;
        var x = 1;
        var sum = 0;
        foreach(var line in reader.ReadAsSpanLineByLine())
        {
            _ = line switch
            {
                ['a', 'd', 'd', 'x', ' ', ..var amount] => AddX(int.Parse(amount)),
                "noop" => Noop(),
                _ => throw new NotSupportedException()
            };
        }

        return sum;

        bool Noop()
        {
            IncrementCycle();
            return true;
        }

        bool AddX(int amount)
        {
            IncrementCycle();
            IncrementCycle();
            x += amount;
            return true;
        }

        void IncrementCycle()
        {
            cycle++;
            if((cycle - 20) % 40 == 0)
            {
                sum += cycle * x;
            }
        }
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var cycle = 0;
        var x = 1;
        var sb = new StringBuilder();
        foreach (var line in reader.ReadAsSpanLineByLine())
        {
            _ = line switch
            {
                ['a', 'd', 'd', 'x', ' ', .. var amount] => AddX(int.Parse(amount)),
                "noop" => Noop(),
                _ => throw new NotSupportedException()
            };
        }

        return sb.ToString();

        bool Noop()
        {
            IncrementCycle();
            return true;
        }

        bool AddX(int amount)
        {
            IncrementCycle();
            IncrementCycle();
            x += amount;
            return true;
        }

        void IncrementCycle()
        {
            var currentPosition = cycle % 40;
            if(Math.Abs(currentPosition - x) <= 1)
            {
                sb.Append('#');
            }
            else
            {
                sb.Append('.');
            }
            cycle++;
            if (cycle % 40 == 0)
            {
                sb.AppendLine();
            }
        }
    }
}
