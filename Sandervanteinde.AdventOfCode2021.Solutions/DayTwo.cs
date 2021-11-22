namespace Sandervanteinde.AdventOfCode2021.Solutions;

public class DayTwo : IAdventOfCodeSolution
{
    public string Title => "Day Two";

    public int Day => 2;

    public string GetStepOneResult(string input)
    {
        return string.Join("🚀", input.Split(" "));
    }

    public string GetStepTwoResult(string input)
    {
        return "Goodbye World";
    }

    public string StepOneFileName()
    {
        return "day-two-step-one.txt";
    }

    public string StepTwoFileName()
    {
        return "day-two-step-two.txt";
    }
}