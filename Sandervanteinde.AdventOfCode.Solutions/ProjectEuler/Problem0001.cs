namespace Sandervanteinde.AdventOfCode.Solutions.ProjectEuler;

internal class Problem0001 : IProjectEulerSolution
{
    public string Title => "Multiples of 3 or 5";

    public int Id => 1;

    public object GetSolution()
    {
        var sum = 0;

        for (var i = 0; i < 1000; i++)
        {
            if (i % 3 == 0 || i % 5 == 0)
            {
                sum += i;
            }
        }

        return sum;
    }
}
