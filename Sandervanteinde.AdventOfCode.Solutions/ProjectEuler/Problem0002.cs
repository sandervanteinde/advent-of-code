namespace Sandervanteinde.AdventOfCode.Solutions.ProjectEuler;

internal class Problem0002 : IProjectEulerSolution
{
    public string Title => "Even Fibonacci numbers";
    public int Id => 2;

    public object GetSolution()
    {
        return Fibonacci.Enumerate()
            .TakeWhile(x => x <= 4_000_000)
            .Where(x => x % 2 == 0)
            .Sum();
    }
}
