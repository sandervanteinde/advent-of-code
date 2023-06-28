namespace Sandervanteinde.AdventOfCode.Solutions.ProjectEuler;

public class Problem0006 : IProjectEulerSolution
{
    public string Title => "Sum Square Difference";
    public int Id => 6;
    public object GetSolution()
    {
        var sumOfPowers = 0.0;
        var sum = 0;
        for (var i = 1; i <= 100; i++)
        {
            sumOfPowers += Math.Pow(i, 2);
            sum += i;
        }

        var sumSquared = Math.Pow(sum, 2);
        return sumSquared - sumOfPowers;
    }
}
