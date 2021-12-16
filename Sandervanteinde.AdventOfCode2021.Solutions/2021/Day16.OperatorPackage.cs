using Sandervanteinde.AdventOfCode2021.Solutions.Extensions;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day16
{
    public class OperatorPackage : Package
    {
        private readonly List<Package> packages = new();
        public IReadOnlyCollection<Package> Packages => packages.AsReadOnly();

        public OperatorPackage(long version, PackageTypeId packageTypeId)
            : base(version, packageTypeId)
        { }

        public void Add(Package package)
        {
            packages.Add(package);
        }

        public override long VersionSum()
        {
            return Version + Packages.Sum(p => p.VersionSum());
        }

        public override long CalculateValue()
        {
            return PackageTypeId switch
            {
                PackageTypeId.Sum => packages.Sum(p => p.CalculateValue()),
                PackageTypeId.Product => packages.Product(p => p.CalculateValue()),
                PackageTypeId.Minimum => packages.Min(p => p.CalculateValue()),
                PackageTypeId.Maximum => packages.Max(p => p.CalculateValue()),
                PackageTypeId.GreaterThan => packages[0].CalculateValue() > packages[1].CalculateValue() ? 1 : 0,
                PackageTypeId.LessThan => packages[0].CalculateValue() < packages[1].CalculateValue() ? 1 : 0,
                PackageTypeId.EqualTo => packages[0].CalculateValue() == packages[1].CalculateValue() ? 1 : 0,
                _ => throw new InvalidOperationException("Invalid package type id")
            };
        }

        public override string ToString()
        {
            return $"{PackageTypeId} of ({string.Join(", ", packages.Select(p => p.ToString()))})";
        }
    }
}
