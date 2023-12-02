namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day24
{
    public class EqlOperation : IOperation
    {
        public long PerformOperation(long left, long right)
        {
            return left == right
                ? 1
                : 0;
        }
    }
}
