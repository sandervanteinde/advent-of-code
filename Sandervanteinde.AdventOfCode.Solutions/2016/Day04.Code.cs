using System.Text;

namespace Sandervanteinde.AdventOfCode.Solutions._2016;

internal partial class Day04
{
    public class Code
    {
        public string Name { get; init; }
        public string Checksum { get; init; }
        public int SectorId { get; init; }

        public bool IsValidChecksum()
        {
            var characterCount = Name
                .Where(c => c != '-')
                .GroupBy(c => c)
                .OrderBy(c => c, new GroupingComparer());

            var i = 0;

            foreach (var letter in characterCount)
            {
                if (Checksum[i] != letter.Key)
                {
                    return false;
                }

                i++;

                if (Checksum.Length == i)
                {
                    return true;
                }
            }

            return true;
        }

        public string Decipher()
        {
            var sb = new StringBuilder();

            foreach (var c in Name)
            {
                if (c == '-')
                {
                    sb.Append(value: ' ');
                    continue;
                }

                sb.Append((char)(((c - 97 + SectorId) % 26) + 97));
            }

            return sb.ToString();
        }

        private class GroupingComparer : IComparer<IGrouping<char, char>>
        {
            public int Compare(IGrouping<char, char>? x, IGrouping<char, char>? y)
            {
                var countLeft = x.Count();
                var countRight = y.Count();

                if (countLeft == countRight)
                {
                    return x.Key.CompareTo(y.Key);
                }

                return countRight.CompareTo(countLeft);
            }
        }
    }
}
