namespace Sandervanteinde.AdventOfCode2021.Solutions;

public class SolutionRegistry
{
    private readonly List<IAdventOfCodeSolution> _solutions = new();
    public IEnumerable<int> AvailableYears => _solutions
        .Select(sln => sln.Year)
        .Distinct()
        .OrderBy(year => year);

    internal void RegisterSolution(IAdventOfCodeSolution solution)
    {
        _solutions.Add(solution);
    }

    public IEnumerable<int> DaysForYear(int year)
    {
        return _solutions
            .Where(solution => solution.Year == year)
            .Select(solution => solution.Day);
    }

    public IAdventOfCodeSolution GetSolution(int year, int day)
    {
        return _solutions.First(sln => sln.Year == year && sln.Day == day);
    }
}
