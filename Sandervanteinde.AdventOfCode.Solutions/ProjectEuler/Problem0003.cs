namespace Sandervanteinde.AdventOfCode.Solutions.ProjectEuler;

internal class Problem0003 : IProjectEulerSolution
{
    public string Title => "Largest prime factor";

    public int Id => 3;

    public object GetSolution()
    {
        return Primes.FactorsOf(value: 600851475143)
            .Max();
    }
}
