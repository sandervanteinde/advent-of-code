using System.Text;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day16
{
    public abstract class Package
    {
        public long Version { get; }
        public PackageTypeId PackageTypeId { get; }

        protected Package(long version, PackageTypeId packageTypeId)
        {
            Version = version;
            PackageTypeId = packageTypeId;
        }

        public abstract long VersionSum();
        public abstract long CalculateValue();
        public abstract void BinaryRepresentation(StringBuilder sb);

        public string ToBits(long i)
        {
            return Convert.ToString(i, 2);
        }

        public string Prepend(string str, int totalAmount)
        {
            var missing = totalAmount - str.Length;
            if (missing <= 0)
            {
                return str;
            }

            return $"{new string('0', missing)}{str}";
        }
    }
}
