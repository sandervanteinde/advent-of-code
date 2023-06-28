using System.Buffers;
using System.Security.Cryptography;
using System.Text;

namespace Sandervanteinde.AdventOfCode.Solutions._2016;

internal class Day05 : BaseSolution
{
    public Day05()
        : base(@"How About a Nice Game of Chess?", 2016, 5)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        using var md5 = MD5.Create();
        var sb = new StringBuilder();
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

            if (isFirstFiveBitsZero)
            {
                sb.Append(Convert.ToString(hash[2], 16));
                if (sb.Length == 8)
                {
                    break;
                }
            }

            ArrayPool<byte>.Shared.Return(bytes);
        }
        return sb.ToString();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        using var md5 = MD5.Create();
        var password = new char[8];
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

            if (isFirstFiveBitsZero)
            {
                var index = hash[2];
                if (index < 8 && password[index] == default)
                {
                    var result = hash[3] >> 4;
                    password[index] = Convert.ToString(result, 16)[0];
                    if (password.All(p => p != default))
                    {
                        break;
                    }
                }
            }

            ArrayPool<byte>.Shared.Return(bytes);
        }
        return new string(password);
    }
}
