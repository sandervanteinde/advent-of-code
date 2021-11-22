namespace Sandervanteinde.AdventOfCode2021.Solutions;

public class DayOne : IAdventOfCodeSolution
{
    public string Title => "Day One";

    public int Day => 1;

    public string GetStepOneResult(string input)
    {
        return string.Join("❄️", input.Split(" "));
    }

    public string GetStepTwoResult(string input)
    {
        return "Hello World";
    }

    public string StepOneFileName()
    {
        return "day-one.txt";
    }

    public string StepTwoFileName()
    {
        return "day-one.txt";
    }
}
