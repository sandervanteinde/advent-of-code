namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

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
    }
}
