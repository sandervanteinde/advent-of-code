using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2016;

internal partial class Day07
{
    public class Ipv7Address
    {
        private static readonly Regex regex = new(@"([a-z]+)\[([a-z]+)\](.+)");
        private static readonly Regex abaRegex = new(@"(.)(.)\1");
        private static readonly Regex abbaRegex = new Regex(@"(.)(.)\2\1");

        private readonly List<string> outsideSquareBracket = new();
        private readonly List<string> insideSquareBracket = new();
        public IReadOnlyCollection<string> OutsideSquareBracket => outsideSquareBracket.AsReadOnly();
        public IReadOnlyCollection<string> InsideSquareBracket => insideSquareBracket.AsReadOnly();
        public Ipv7Address(string str)
        {
            var match = regex.Match(str);
            while (match.Success)
            {
                outsideSquareBracket.Add(match.Groups[1].Value);
                insideSquareBracket.Add(match.Groups[2].Value);
                str = match.Groups[3].Value;
                match = regex.Match(str);
            }
            outsideSquareBracket.Add(str);
        }

        public bool SupportsTls()
        {
            return outsideSquareBracket.Any(IsAbbaMatch)
                && insideSquareBracket.All(b => !IsAbbaMatch(b));
        }

        private static bool IsAbbaMatch(string str)
        {
            var matches = abbaRegex.Matches(str);
            return matches.Any(b => b.Groups[1].Value != b.Groups[2].Value);
        }

        public bool SupportsSsl()
        {
            var abas = outsideSquareBracket.SelectMany(OverlappingMatches);
            return abas
                .Where(s => s.Groups[1].Value != s.Groups[2].Value)
                .Select(s => $"{s.Groups[2].Value}{s.Groups[1].Value}{s.Groups[2].Value}")
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