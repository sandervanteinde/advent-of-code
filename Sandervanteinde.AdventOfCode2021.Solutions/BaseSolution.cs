using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandervanteinde.AdventOfCode2021.Solutions;

internal abstract class BaseSolution : IAdventOfCodeSolution
{
    public string Title { get; }
    public int Day { get; }
    public  int Year { get; }

    private readonly string _fileNameOne;
    private readonly string _fileNameTwo;

    public BaseSolution(string title, int year, int day, string fileName = null, string? fileNameTwo = null)
    {
        Title = title;
        Day = day;
        Year = year;
        _fileNameOne = fileName ?? $"{year}/{day}.txt";
        _fileNameTwo = fileNameTwo ?? _fileNameOne;
    }

    public string GetStepOneResult(string input)
    {
        return DetermineStepOneResult(new(input)).ToString()!;
    }

    public string GetStepTwoResult(string input)
    {
        return DetermineStepTwoResult(new(input)).ToString()!;
    }

    public abstract object DetermineStepOneResult(FileReader reader);
    public abstract object DetermineStepTwoResult(FileReader reader);

    public string StepOneFileName()
    {
        return _fileNameOne;
    }

    public string StepTwoFileName()
    {
        return _fileNameTwo;
    }
}
