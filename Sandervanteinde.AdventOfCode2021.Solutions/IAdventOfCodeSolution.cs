namespace Sandervanteinde.AdventOfCode2021.Solutions;

public interface IAdventOfCodeSolution
{
    string Title { get; }
    int Day { get; }
    int Year { get; }

    string GetStepOneResult(string input);
    string GetStepTwoResult(string input);

    string StepOneFileName();
    string StepTwoFileName();
}
