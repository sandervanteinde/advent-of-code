namespace Sandervanteinde.AdventOfCode2021.Solutions.Utils;

internal class Fibonacci
{
    public static IEnumerable<int> Enumerate(bool startWithDoubleOne = false)
    {
        int current = 1, next = startWithDoubleOne ? 1 : 2;

        while (true)
        {
            yield return current;
            next = current + (current = next);
        }
    }
}
