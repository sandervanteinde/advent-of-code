﻿using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2016;

internal partial class Day04 : BaseSolution
{
    public Day04()
        : base("Security Through Obscurity", year: 2016, day: 4)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        return ParseCodes(reader)
            .Where(code => code.IsValidChecksum())
            .Sum(code => code.SectorId);
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        foreach (var cipher in ParseCodes(reader)
                     .Where(code => code.IsValidChecksum()))
        {
            var deciphered = cipher.Decipher();

            if (deciphered == "northpole object storage")
            {
                return cipher.SectorId;
            }
        }

        throw new InvalidOperationException("Code not found");
    }

    private static IEnumerable<Code> ParseCodes(FileReader reader)
    {
        var regex = new Regex(@"([a-z-]+)-(\d+)\[([a-z]+)\]");

        foreach (var match in reader.MatchLineByLine(regex))
        {
            yield return new Code
            {
                Name = match.Groups[groupnum: 1].Value, Checksum = match.Groups[groupnum: 3].Value, SectorId = int.Parse(match.Groups[groupnum: 2].Value)
            };
        }
    }
}
