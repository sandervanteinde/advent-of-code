namespace Sandervanteinde.AdventOfCode.Solutions.ProjectEuler;

public class Problem0005 : IProjectEulerSolution
{
    public string Title => "Smallest Multiple";
    public int Id => 5;
    public object GetSolution()
    {
        for (var i = 20; i < int.MaxValue; i += 20)
        {
            bool match = true;
            for (var j = 2; j < 20; j++)
            {
                if ((i % j) != 0)
                {
                    match = false;
                    break;
                }
            }

            if (match)
            {
                return i;
            }
        }

        throw new Exception("No solution found");
    }
}
