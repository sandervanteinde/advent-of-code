namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day24
{
    public class ComputerValues
    {
        public long W { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public ComputerValues Clone()
        {
            return new ComputerValues { W = W, X = X, Y = Y, Z = Z };
        }

        public override string ToString()
        {
            return $"{{ W = {W}, X = {X}, Y = {Y}, Z = {Z} }}";
        }
    }
}
