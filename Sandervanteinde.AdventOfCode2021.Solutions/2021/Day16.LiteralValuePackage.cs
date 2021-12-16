using System.Text;

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

        public override void BinaryRepresentation(StringBuilder sb)
        {
            sb.Append(Prepend(ToBits(Version), 3));
            sb.Append(Prepend(ToBits((long)PackageTypeId), 3));

            var number = ToBits(Value);
            var prePad = 4 - (number.Length % 4);
            number = $"{new string('0', prePad)}{number}";

            for (var i = 0; i < number.Length; i += 4)
            {
                sb.Append(i + 4 == number.Length ? '0' : '1');
                sb.Append(number[i]);
                sb.Append(number[i + 1]);
                sb.Append(number[i + 2]);
                sb.Append(number[i + 3]);
            }
        }
    }
}
