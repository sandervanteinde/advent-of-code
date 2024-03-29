﻿using System.Collections;
using System.Text;
using Sandervanteinde.AdventOfCode.Solutions.Extensions;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day16
{
    public class OperatorPackage : Package, IEnumerable<Package>
    {
        private readonly List<Package> packages = new();

        public OperatorPackage(long version, PackageTypeId packageTypeId)
            : base(version, packageTypeId)
        {
        }

        public IReadOnlyCollection<Package> Packages => packages.AsReadOnly();

        public IEnumerator<Package> GetEnumerator()
        {
            return packages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

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
                PackageTypeId.GreaterThan => packages[index: 0]
                    .CalculateValue() > packages[index: 1]
                    .CalculateValue()
                    ? 1
                    : 0,
                PackageTypeId.LessThan => packages[index: 0]
                    .CalculateValue() < packages[index: 1]
                    .CalculateValue()
                    ? 1
                    : 0,
                PackageTypeId.EqualTo => packages[index: 0]
                    .CalculateValue() == packages[index: 1]
                    .CalculateValue()
                    ? 1
                    : 0,
                _ => throw new InvalidOperationException("Invalid package type id")
            };
        }

        public override string ToString()
        {
            return $"{PackageTypeId} of ({string.Join(", ", packages.Select(p => p.ToString()))})";
        }

        public override void BinaryRepresentation(StringBuilder sb)
        {
            sb.Append(Prepend(ToBits(Version), totalAmount: 3));
            sb.Append(Prepend(ToBits((long)PackageTypeId), totalAmount: 3));
            sb.Append(
                Version % 2 == 0
                    ? '1'
                    : '0'
            );

            if (Version % 2 == 0)
            {
                sb.Append(Prepend(ToBits(packages.Count), totalAmount: 11));

                foreach (var package in packages)
                {
                    package.BinaryRepresentation(sb);
                }

                return;
            }

            var innerSb = new StringBuilder();

            foreach (var package in packages)
            {
                package.BinaryRepresentation(innerSb);
            }

            sb.Append(Prepend(ToBits(innerSb.Length), totalAmount: 15));
            sb.Append(innerSb);
        }
    }
}
