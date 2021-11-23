using Microsoft.Extensions.DependencyInjection;

namespace Sandervanteinde.AdventOfCode2021.Solutions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSolutions(this IServiceCollection services)
    {
        var solutionType = typeof(IAdventOfCodeSolution);
        var solutions = typeof(ServiceCollectionExtensions).Assembly.GetTypes()
            .Where(type => type.IsAssignableTo(solutionType) && type is { IsAbstract: false, IsInterface: false})
            .ToArray();

        var registry = new SolutionRegistry();
        foreach (var solution in solutions)
        {
            var instance = (IAdventOfCodeSolution)Activator.CreateInstance(solution)!;
            registry.RegisterSolution(instance);
        }

        services.AddSingleton(registry);
        return services;
    }
}
