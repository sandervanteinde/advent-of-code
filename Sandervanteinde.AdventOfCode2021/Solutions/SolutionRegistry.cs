namespace Sandervanteinde.AdventOfCode2021.Solutions;

public class SolutionRegistry
{
    private readonly List<IAdventOfCodeSolution> _solutions = new();
    public IEnumerable<IAdventOfCodeSolution> Solutions => _solutions.AsReadOnly();

    internal void RegisterSolution(IAdventOfCodeSolution solution)
    {
        _solutions.Add(solution);
    }
}
