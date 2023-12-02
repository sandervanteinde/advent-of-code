namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal class Day20 : BaseSolution
{
    public Day20()
        : base("Infinite Elves and Infinite Houses", year: 2015, day: 20)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var target = int.Parse(reader.Input);
        var houses = new int[(target / 10) + 1];

        for (var elf = 1; elf < houses.Length; elf++)
        {
            for (var house = elf; house < houses.Length; house += elf)
            {
                houses[house] += elf * 10;
            }
        }

        for (var house = 1; house < houses.Length; house++)
        {
            if (houses[house] > target)
            {
                return house;
            }
        }

        throw new InvalidOperationException();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var target = int.Parse(reader.Input);
        var houses = new int[(target / 11) + 1];

        for (var elf = 1; elf < houses.Length; elf++)
        {
            var i = 0;

            for (var house = elf; house < houses.Length && i < 50; house += elf, i++)
            {
                houses[house] += elf * 11;
            }
        }

        for (var house = 1; house < houses.Length; house++)
        {
            if (houses[house] > target)
            {
                return house;
            }
        }

        throw new InvalidOperationException();
    }
}
