using Sandervanteinde.AdventOfCode.Solutions.Extensions;

namespace Sandervanteinde.AdventOfCode.Solutions.ProjectEuler;

internal class Problem0004 : IProjectEulerSolution
{
    public string Title => "Largest Palindrome Product";
    public int Id => 4;
    public object GetSolution()
    {
        var largestSolution = int.MinValue;
        for (var i = 999; i >= 1; i--)
        {
            for (var j = i - 1; j >= 1; j--)
            {
                if ((i * j).ToString().IsPalindrome())
                {
                    largestSolution = Math.Max(largestSolution, i * j);
                }
            }
        }

        return largestSolution;
    }
}
