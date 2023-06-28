namespace Sandervanteinde.AdventOfCode.Solutions._2022;

internal class Day01 : BaseSolution
{
    public Day01()
        : base("Calorie Counting", 2022, 1)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var lines = reader.ReadLineByLine(StringSplitOptions.None);
        var sum = 0;
        var highestSum = 0;
        foreach(var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                highestSum = Math.Max(highestSum, sum);
                sum = 0;
                continue;
            }
            sum += int.Parse(line);
        }
        return highestSum;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var lines = reader.ReadLineByLine(StringSplitOptions.None);
        var sum = 0;
        var calories = new List<int>();
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                calories.Add(sum);
                sum = 0;
                continue;
            }
            sum += int.Parse(line);
        }
        if(sum != 0)
        {
            calories.Add(sum);
        }
        return calories.OrderByDescending(c => c)
            .Take(3)
            .Sum();
    }
}
