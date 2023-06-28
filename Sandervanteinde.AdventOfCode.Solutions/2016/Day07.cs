using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2016;

internal partial class Day07 : BaseSolution
{
    public Day07()
        : base(@"Internet Protocol Version 7", 2016, 7)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var regex = new Regex(@"(.)(.)\2\1");
        return ParseIpv7Addresses(reader)
            .Where(address => address.SupportsTls())
            .Count();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var regex = new Regex(@"(.)(.)\1");
        return ParseIpv7Addresses(reader)
            .Where(address => address.SupportsSsl())
            .Count();
    }

    private static IEnumerable<Ipv7Address> ParseIpv7Addresses(FileReader reader)
    {
        foreach (var line in reader.ReadLineByLine())
        {
            yield return new Ipv7Address(line);
        }
    }
}