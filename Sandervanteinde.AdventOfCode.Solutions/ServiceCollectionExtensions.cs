using Microsoft.Extensions.DependencyInjection;
using Sandervanteinde.AdventOfCode.Solutions.ProjectEuler;

namespace Sandervanteinde.AdventOfCode.Solutions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSolutions(this IServiceCollection services)
    {
        var solutionType = typeof(IAdventOfCodeSolution);
        var solutions = typeof(ServiceCollectionExtensions).Assembly.GetTypes()
            .Where(type => type.IsAssignableTo(solutionType) && type is { IsAbstract: false, IsInterface: false })
            .ToArray();

        var projectEulerInterface = typeof(IProjectEulerSolution);
        var projectEulerSolutions = typeof(ServiceCollectionExtensions).Assembly.GetTypes()
            .Where(type => type.IsAssignableTo(projectEulerInterface) && type is { IsAbstract: false, IsInterface: false })
            .ToArray();

        var registry = new SolutionRegistry();
        foreach (var solution in solutions)
        {
            var instance = (IAdventOfCodeSolution)Activator.CreateInstance(solution)!;
            registry.RegisterSolution(instance);
        }

        foreach (var projectEuler in projectEulerSolutions)
        {
            var instance = (IProjectEulerSolution)Activator.CreateInstance(projectEuler)!;
            registry.RegisterSolution(instance);
        }

        services.AddSingleton(registry);
        return services;
    }
}
