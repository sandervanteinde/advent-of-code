using Microsoft.Extensions.DependencyInjection;
using Sandervanteinde.AdventOfCode2021.Solutions;
using TextCopy;

var serviceCollection = new ServiceCollection();
serviceCollection.AddSolutions();
var serviceProvider = serviceCollection.BuildServiceProvider();

var solutionRegistry = serviceProvider.GetRequiredService<SolutionRegistry>();
var latestCommand = args.FirstOrDefault(arg => arg.StartsWith("--run-latest"));
if (latestCommand is not null)
{
    var latestYear = latestCommand.Contains("=")
        ? int.Parse(latestCommand.Split('=')[1])
        : solutionRegistry.AvailableYears.Max();
    var latestDAy = solutionRegistry.DaysForYear(latestYear).Max();
    var solution = solutionRegistry.GetSolution(latestYear, latestDAy);

    Console.WriteLine($"Answers for Solution {solution.Year} - {solution.Day}");
    Console.WriteLine("Step one:");
    var input = await File.ReadAllTextAsync(Path.Combine($"..\\..\\..\\..\\Sandervanteinde.AdventOfCode2021\\wwwroot\\sample-data\\", solution.StepOneFileName()));
    var result = solution.GetStepOneResult(input);
    Console.WriteLine(result);
    await new Clipboard().SetTextAsync(result);

    Console.WriteLine("Press any key to continue");
    Console.WriteLine();
    Console.Read();

    Console.WriteLine("Step two:");
    input = await File.ReadAllTextAsync(Path.Combine($"..\\..\\..\\..\\Sandervanteinde.AdventOfCode2021\\wwwroot\\sample-data\\", solution.StepTwoFileName()));
    result = solution.GetStepTwoResult(input);
    Console.WriteLine(result);
    await new Clipboard().SetTextAsync(result);
    return 0;
}
while (true)
{
    Console.Clear();
    try
    {
        Console.WriteLine("Write the year");
        Console.Write("--> ");
        var year = int.Parse(Console.ReadLine());

        Console.WriteLine("Write the test number");
        Console.Write("--> ");
        var testNumber = int.Parse(Console.ReadLine());

        var solution = solutionRegistry.GetSolution(year, testNumber);
        if (solution == null)
        {
            Console.Error.WriteLine("Unknown solution");
            return 1;
        }

        Console.WriteLine("Paste the file input or file location");
        Console.Write("--> ");
        var fileOrContents = Console.ReadLine()!;

        if (File.Exists(fileOrContents))
        {
            fileOrContents = File.ReadAllText(fileOrContents);
        }

        Console.WriteLine("Step one or step two");
        var oneOrTwo = int.Parse(Console.ReadLine()!);
        var result = oneOrTwo switch
        {
            1 => solution.GetStepOneResult(fileOrContents),
            2 => solution.GetStepTwoResult(fileOrContents),
            _ => throw new InvalidOperationException("Invalid file")
        };

        Console.WriteLine("Result:");
        Console.WriteLine(result);
        var clipboard = new Clipboard();
        clipboard.SetText(result);

        return 0;
    }
    catch { }
}
