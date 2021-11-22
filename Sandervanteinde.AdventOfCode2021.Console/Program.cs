using Microsoft.Extensions.DependencyInjection;
using Sandervanteinde.AdventOfCode2021.Solutions;
using TextCopy;

var serviceCollection = new ServiceCollection();
serviceCollection.AddSolutions();
var serviceProvider = serviceCollection.BuildServiceProvider();

var solutionRegistry = serviceProvider.GetRequiredService<SolutionRegistry>();

Console.WriteLine("Write the test number");
Console.Write("--> ");
var testNumber = int.Parse(Console.ReadLine());

var solution = solutionRegistry.Solutions.First(sln => sln.Day == testNumber);
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