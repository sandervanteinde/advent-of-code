using System.Collections;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day16 : BaseSolution
{
    public Day16()
        : base("Packet Decoder", 2021, 16)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var bitReader = ToBitReader(reader);
        var package = ReadPackage(bitReader);
        return package.VersionSum();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var bitReader = ToBitReader(reader);
        var package = ReadPackage(bitReader);
        return package.CalculateValue();
    }

    private static Package ReadPackage(BitReader reader)
    {

        var packageVersion = reader.ReadAsNumber(3);
        var packetTypeId = (PackageTypeId)reader.ReadAsNumber(3);

        return packetTypeId switch
        {
            PackageTypeId.Literal => HandleLiteralPackage(packageVersion, reader),
            _ => HandleOperatorPackage(packageVersion, packetTypeId, reader)
        };
    }

    private static LiteralValuePackage HandleLiteralPackage(long packageVersion, BitReader reader)
    {
        var items = new LinkedList<bool[]>();
        var sections = 0;
        while (true)
        {
            var nextSection = reader.Read(5);
            sections++;

            items.AddLast(nextSection[1..].ToArray());

            if (!nextSection[0])
            {
                break;
            }
        }

        var literalValue = new BitReader(items.SelectMany(x => x))
            .ReadAsNumber(sections * 4);
        return new LiteralValuePackage(packageVersion, literalValue);
    }

    private static OperatorPackage HandleOperatorPackage(long packageVersion, PackageTypeId packageTypeId, BitReader reader)
    {
        var operatorPackage = new OperatorPackage(packageVersion, packageTypeId);
        if (reader.ReadNext())
        {
            var numberOfSubpackets = reader.ReadAsNumber(11);
            for (var i = 0; i < numberOfSubpackets; i++)
            {
                operatorPackage.Add(ReadPackage(reader));
            }
            return operatorPackage;
        }

        var subpacketLength = (int)reader.ReadAsNumber(15);
        var innerReader = new BitReader(reader.Read(subpacketLength).ToArray());
        while (innerReader.HasMore)
        {
            operatorPackage.Add(ReadPackage(innerReader));
        }
        return operatorPackage;
    }

    public static BitReader ToBitReader(FileReader reader)
    {
        return new BitReader(ToBitStream(reader));
    }

    public static IEnumerable<bool> ToBitStream(FileReader reader)
    {
        foreach (var c in reader.ReadCharByChar())
        {
            var bits = c switch
            {
                >= '0' and <= '9' => ToBits(c - 48),
                >= 'A' and <= 'F' => ToBits(c - 55),
                _ => throw new InvalidOperationException("Invalid character encountered")
            };
            foreach (var bit in bits)
            {
                yield return bit;
            }
        }

        static IEnumerable<bool> ToBits(int @byte)
        {
            var bitArray = new BitArray(new byte[] { (byte)@byte });
            for (var i = 3; i >= 0; i--)
            {
                yield return bitArray[i];
            }
        }
    }
}
