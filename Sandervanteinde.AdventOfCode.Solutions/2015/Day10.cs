using System.Text;
using Sandervanteinde.AdventOfCode.Solutions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day10 : BaseSolution
{

    public Day10()
        : base("Elves Look, Elves Say", 2015, 10)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        return ApplyXTimes(40, reader);
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        return ApplyXTimes(50, reader);
    }
    private int ApplyXTimes(int x, FileReader reader)
    {
        for (var i = 0; i < x; i++)
        {
            var sb = new StringBuilder();
            char? lastChar = null;
            var count = 0;
            foreach (var c in reader.ReadCharByChar())
            {
                if (lastChar is null)
                {
                    lastChar = c;
                    count = 1;
                    continue;
                }

                if (lastChar != c)
                {
                    sb.Append(count);
                    sb.Append(lastChar);
                    lastChar = c;
                    count = 1;
                }
                else
                {
                    count++;
                }
            }
            sb.Append(count);
            sb.Append(lastChar);
            reader = new(sb.ToString());
        }

        return reader.Input.Length;

    }
}
