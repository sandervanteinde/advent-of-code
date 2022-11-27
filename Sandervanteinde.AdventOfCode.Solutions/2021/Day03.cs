using Sandervanteinde.AdventOfCode.Solutions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal class Day03 : BaseSolution
{
    private const int BITS = 12;
    public Day03()
        : base("Binary Diagnostic", 2021, 3)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var list = Enumerable.Repeat(0, BITS).Select(i => new OnOrOff { On = 0, Off = 0 }).ToList();
        var bitValues = Enumerable.Repeat(0, BITS).Select((_, index) => (int)(Math.Pow(2, index))).ToList();
        foreach (var bits in ParseInput(reader))
        {
            for (var i = 0; i < BITS; i++)
            {
                var isOn = (bits & bitValues[i]) == bitValues[i];
                if (isOn)
                {
                    list[i].On++;
                }
                else
                {
                    list[i].Off++;
                }
            }
        }
        var linkedList = new LinkedList<char>();
        foreach (var item in list)
        {
            linkedList.AddFirst(item.Off > item.On ? '0' : '1');
        }
        var reversed = linkedList.Select(i => i == '0' ? '1' : '0');
        var gammaRate = Convert.ToInt32(new string(linkedList.ToArray()), 2);
        var epsilonRate = Convert.ToInt32(new string(reversed.ToArray()), 2);
        return gammaRate * epsilonRate;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var list = Enumerable.Repeat(0, BITS).Select(i => new OnOrOff { On = 0, Off = 0 }).ToList();
        var bitValues = Enumerable.Repeat(0, BITS).Select((_, index) => (int)(Math.Pow(2, index))).Reverse().ToList();
        var oxygenGeneratorRating = ParseInput(reader).ToList();
        var co2ScrubberRating = new List<int>(oxygenGeneratorRating);

        foreach (var bitValue in bitValues)
        {
            if (oxygenGeneratorRating.Count > 1)
            {
                oxygenGeneratorRating = DetermineNewValue(oxygenGeneratorRating, bitValue, false);
            }
            if (co2ScrubberRating.Count > 1)
            {
                co2ScrubberRating = DetermineNewValue(co2ScrubberRating, bitValue, true);
            }
        }

        return oxygenGeneratorRating.Single() * co2ScrubberRating.Single();

        List<int> ExcludingBit(int bit, List<int> list) => list.Where(b => (b & bit) == 0).ToList();

        List<int> IncludingBit(int bit, List<int> list) => list.Where(b => (b & bit) == bit).ToList();

        List<int> DetermineNewValue(List<int> oxygenGeneratorRating, int bitValue, bool inversed)
        {
            var onOrOff = new OnOrOff();
            foreach (var item in oxygenGeneratorRating)
            {
                if ((item & bitValue) == bitValue)
                {
                    onOrOff.On++;
                }
                else
                {
                    onOrOff.Off++;
                }
            }
            Func<int, List<int>, List<int>> returnMethod = onOrOff.On >= onOrOff.Off
                ? IncludingBit
                : ExcludingBit;
            if (inversed)
            {
                returnMethod = returnMethod == IncludingBit ? ExcludingBit : IncludingBit;
            }
            return returnMethod(bitValue, oxygenGeneratorRating);
        }
    }

    private static IEnumerable<int> ParseInput(FileReader reader)
    {
        foreach (var line in reader.ReadLineByLine())
        {
            yield return Convert.ToInt32(line, 2);
        }
    }

    private class OnOrOff
    {
        public int On { get; set; }
        public int Off { get; set; }
    }
}
