namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day24
{
    public class ModOperation : IOperation
    {
        public long PerformOperation(long left, long right)
        {
            return left % right;
        }
    }
}
