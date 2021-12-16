namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day16
{
    public class LiteralValuePackage : Package
    {
        public long Value { get; }

        public LiteralValuePackage(long version, long value)
            : base(version, PackageTypeId.Literal)
        {
            Value = value;
        }

        public override long CalculateValue()
        {
            return Value;
        }

        public override long VersionSum()
        {
            return Version;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
