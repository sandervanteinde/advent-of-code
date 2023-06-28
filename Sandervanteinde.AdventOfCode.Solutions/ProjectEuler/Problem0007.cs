using System.Text;

namespace Sandervanteinde.AdventOfCode.Solutions.ProjectEuler;

public class Problem0007 : IProjectEulerSolution
{
    public string Title => "10001st Prime";
    public int Id => 7;
    public object GetSolution()
    {
        return Primes.Enumerate(100_000_000)
           .Skip(10_000)
           .First();
    }
}
