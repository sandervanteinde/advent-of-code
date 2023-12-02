using Microsoft.Extensions.DependencyInjection;
using Sandervanteinde.AdventOfCode.Solutions;
using TextCopy;

var serviceCollection = new ServiceCollection();
serviceCollection.AddSolutions();
var serviceProvider = serviceCollection.BuildServiceProvider();

var solutionRegistry = serviceProvider.GetRequiredService<SolutionRegistry>();
var latestCommand = args.FirstOrDefault(arg => arg.StartsWith("--run-latest"));
var dayCommand = args.FirstOrDefault(arg => arg.StartsWith("--day="));
var euler = args.FirstOrDefault(Arg => Arg.StartsWith("--project-euler"));

if (euler is not null)
{
    var latest = solutionRegistry.ProjectEuler.MaxBy(x => x.Id)
        ?? throw new InvalidOperationException();
    Console.WriteLine(latest.Title);
    Console.WriteLine("Answer: ");
    var sln = latest.GetSolution();
    Console.WriteLine(sln);
    await new Clipboard().SetTextAsync(sln.ToString() ?? "");
    return 0;
}

if (latestCommand is not null)
{
    var year = latestCommand.Contains("=")
        ? int.Parse(latestCommand.Split(separator: '=')[1])
        : solutionRegistry.AvailableYears.Max();
    var day = dayCommand is null
        ? solutionRegistry.DaysForYear(year)
            .Max()
        : int.Parse(dayCommand.Split(separator: '=')[1]);
    var solution = solutionRegistry.GetSolution(year, day);

    Console.WriteLine($"Answers for Solution {solution.Year} - {solution.Day}");
    Console.WriteLine("Step one:");
    var input = await File.ReadAllTextAsync(Path.Combine("..\\..\\..\\..\\Sandervanteinde.AdventOfCode\\wwwroot\\sample-data\\", solution.StepOneFileName()));
    var result = solution.GetStepOneResult(input);
    Console.WriteLine(result);
    await new Clipboard().SetTextAsync(result);

    Console.WriteLine("Press any key to continue");
    Console.WriteLine();
    Console.Read();

    Console.WriteLine("Step two:");
    input = await File.ReadAllTextAsync(Path.Combine("..\\..\\..\\..\\Sandervanteinde.AdventOfCode\\wwwroot\\sample-data\\", solution.StepTwoFileName()));
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
        var year = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException("Expected input"));

        Console.WriteLine("Write the test number");
        Console.Write("--> ");
        var testNumber = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException("Expected input"));

        var solution = solutionRegistry.GetSolution(year, testNumber);

        if (solution == null)
        {
            Console.Error.WriteLine("Unknown solution");
            return 1;
        }

        Console.WriteLine("Step one or step two");
        var oneOrTwo = int.Parse(Console.ReadLine()!);
        var result = oneOrTwo switch
        {
            1 => solution.GetStepOneResult(
                File.ReadAllText(Path.Combine("..\\..\\..\\..\\Sandervanteinde.AdventOfCode\\wwwroot\\sample-data\\", solution.StepOneFileName()))
            ),
            2 => solution.GetStepTwoResult(
                File.ReadAllText(Path.Combine("..\\..\\..\\..\\Sandervanteinde.AdventOfCode\\wwwroot\\sample-data\\", solution.StepTwoFileName()))
            ),
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
