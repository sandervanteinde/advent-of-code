using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using System.Buffers;
using System.Security.Cryptography;
using System.Text;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal class Day04 : BaseSolution
{
    public Day04()
        : base("The Ideal Stocking Stuffer", 2015, 4)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        using var md5 = MD5.Create();
        for (var i = 0; i < int.MaxValue; i++)
        {
            var text = $"{reader.Input}{i}";
            var bytes = ArrayPool<byte>.Shared.Rent(text.Length);
            var readBytes = Encoding.UTF8.GetBytes(text, bytes);
            var hash = md5.ComputeHash(bytes, 0, readBytes);

            var isFirstFiveBitsZero = 
                   hash[0] == 0 
                && hash[1] == 0 
                && hash[2] < 16;

            if(isFirstFiveBitsZero)
            {
                return i;
            }
        }

        throw new InvalidOperationException();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        using var md5 = MD5.Create();
        for (var i = 0; i < int.MaxValue; i++)
        {
            var text = $"{reader.Input}{i}";
            var bytes = ArrayPool<byte>.Shared.Rent(text.Length);
            var readBytes = Encoding.UTF8.GetBytes(text, bytes);
            var hash = md5.ComputeHash(bytes, 0, readBytes);

            var isFirstFiveBitsZero =
                   hash[0] == 0
                && hash[1] == 0
                && hash[2] == 0;

            if (isFirstFiveBitsZero)
            {
                return i;
            }
        }

        throw new InvalidOperationException();
    }
}
