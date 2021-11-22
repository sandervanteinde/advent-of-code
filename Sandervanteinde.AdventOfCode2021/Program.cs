using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sandervanteinde.AdventOfCode2021;
using Sandervanteinde.AdventOfCode2021.Solutions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var solutionType = typeof(IAdventOfCodeSolution);
var solutions = typeof(Program).Assembly.GetTypes()
    .Where(type => type.IsAssignableTo(solutionType) && type != solutionType)
    .ToArray();

var registry = new SolutionRegistry();
foreach (var solution in solutions)
{
    var instance = (IAdventOfCodeSolution)Activator.CreateInstance(solution)!;
    registry.RegisterSolution(instance);
}

builder.Services.AddSingleton(registry);


await builder.Build().RunAsync();
