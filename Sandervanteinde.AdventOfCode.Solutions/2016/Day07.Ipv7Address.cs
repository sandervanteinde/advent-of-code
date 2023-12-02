using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2016;

internal partial class Day07
{
    public class Ipv7Address
    {
        private static readonly Regex regex = new(@"([a-z]+)\[([a-z]+)\](.+)");
        private static readonly Regex abaRegex = new(@"(.)(.)\1");
        private static readonly Regex abbaRegex = new(@"(.)(.)\2\1");
        private readonly List<string> insideSquareBracket = new();

        private readonly List<string> outsideSquareBracket = new();

        public Ipv7Address(string str)
        {
            var match = regex.Match(str);

            while (match.Success)
            {
                outsideSquareBracket.Add(match.Groups[groupnum: 1].Value);
                insideSquareBracket.Add(match.Groups[groupnum: 2].Value);
                str = match.Groups[groupnum: 3].Value;
                match = regex.Match(str);
            }

            outsideSquareBracket.Add(str);
        }

        public IReadOnlyCollection<string> OutsideSquareBracket => outsideSquareBracket.AsReadOnly();
        public IReadOnlyCollection<string> InsideSquareBracket => insideSquareBracket.AsReadOnly();

        public bool SupportsTls()
        {
            return outsideSquareBracket.Any(IsAbbaMatch)
                && insideSquareBracket.All(b => !IsAbbaMatch(b));
        }

        private static bool IsAbbaMatch(string str)
        {
            var matches = abbaRegex.Matches(str);
            return matches.Any(b => b.Groups[groupnum: 1].Value != b.Groups[groupnum: 2].Value);
        }

        public bool SupportsSsl()
        {
            var abas = outsideSquareBracket.SelectMany(OverlappingMatches);
            return abas
                .Where(s => s.Groups[groupnum: 1].Value != s.Groups[groupnum: 2].Value)
                .Select(s => $"{s.Groups[groupnum: 2].Value}{s.Groups[groupnum: 1].Value}{s.Groups[groupnum: 2].Value}")
                .Any(bab => insideSquareBracket.Any(str => str.Contains(bab)));
        }

        private IEnumerable<Match> OverlappingMatches(string s)
        {
            var match = abaRegex.Match(s);

            while (match.Success)
            {
                yield return match;
                match = abaRegex.Match(s, match.Index + 1);
            }
        }
    }
}
