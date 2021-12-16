using FluentAssertions;
using Sandervanteinde.AdventOfCode2021.Solutions._2021;
using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using System;
using System.Linq;
using System.Text;
using Xunit;
using static Sandervanteinde.AdventOfCode2021.Solutions._2021.Day16;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Tests._2021;

public class Day16Tests
{
    private readonly Day16 _sut;

    public Day16Tests()
    {
        _sut = new Day16();
    }

    [Theory]
    [InlineData("8A004A801A8002F478", 16)]
    [InlineData("620080001611562C8802118E34", 12)]
    [InlineData("C0015000016115A2E0802F182340", 23)]
    [InlineData("A0016C880162017C3686B18A3D4780", 31)]
    public void StepOne_ShouldWorkWithExample(string input, int result)
    {
        var reader = new FileReader(input);

        _sut.DetermineStepOneResult(reader).Should().Be(result);
    }

    [Theory]
    [InlineData("C200B40A82", 3)]
    [InlineData("04005AC33890", 54)]
    [InlineData("880086C3E88112", 7)]
    [InlineData("CE00C43D881120", 9)]
    [InlineData("D8005AC2A8F0", 1)]
    [InlineData("F600BC2D8F", 0)]
    [InlineData("9C005AC2F8F0", 0)]
    [InlineData("9C0141080250320F1802104A08", 1)]
    public void StepTwo_ShouldWorkWithExample(string input, int result)
    {
        var reader = new FileReader(input);

        _sut.DetermineStepTwoResult(reader).Should().Be(result);
    }

    [Theory]
    [InlineData("00000010000000001010101110000000001000110010011100100110111110010001001001110100000000000101010011100100101000001100111100101111111000101", 1337)] // sum of above 2
    public void BinaryStringFun(string input, int result)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < input.Length; i += 4)
        {
            var value = 0;
            for (var j = 0; j < 4; j++)
            {
                value <<= 1;
                var index = i + j;
                if (index < input.Length && input[index] == '1')
                {
                    value |= 1;
                }
            }
            sb.Append(Convert.ToString(value, 16));
        }

        _sut.DetermineStepTwoResult(new FileReader(sb.ToString().ToUpper())).Should().Be(result);
    }

    [Fact]
    public void LiteralRepresentation_Binary()
    {
        var literal = new LiteralValuePackage(7, 2021);
        var sb = new StringBuilder();
        literal.BinaryRepresentation(sb);
        var result = sb.ToString();
        result.Should().Be("111100101111111000101");
    }

    [Fact]
    public void Dummy()
    {
        var main = new OperatorPackage(3, PackageTypeId.Product)
        {
            new OperatorPackage(6, PackageTypeId.Minimum)
            {
                new LiteralValuePackage(1, 283),
                new LiteralValuePackage(3, 11),
                new LiteralValuePackage(2, 2008)
            },
            new LiteralValuePackage(1, 3),
            new OperatorPackage(0, PackageTypeId.Sum)
            {
                new LiteralValuePackage(3, 1337),
                new OperatorPackage(5, PackageTypeId.Product)
                {
                    new LiteralValuePackage(1, 331),
                    new LiteralValuePackage(7, 1742)
                }
            }
        };

        var sb = new StringBuilder();
        main.BinaryRepresentation(sb);
        while (sb.Length % 4 != 0)
        {
            sb.Append('0');
        }
        var result = sb.ToString();
        sb = new StringBuilder();
        for (var i = 0; i < result.Length; i += 4)
        {
            var value = 0;
            for (var j = 0; j < 4; j++)
            {
                value <<= 1;
                var index = i + j;
                if (index < result.Length && result[index] == '1')
                {
                    value |= 1;
                }
            }
            sb.Append(Convert.ToString(value, 16));
        }
        result = sb.ToString().ToUpper();

        _sut.DetermineStepTwoResult(new(result)).Should().Be(19071987);
    }

    [Fact]
    public void ToBitStream_ReturnsCorrectBits()
    {
        var input = new FileReader("D2FE28");
        var bits = ToBitStream(input);

        var inputToBoolArray = "110100101111111000101000".Select(c => c == '1').ToArray();

        bits.Should().BeEquivalentTo(inputToBoolArray);
    }

    [Fact]
    public void BitReader_ReadsCorrectBits()
    {
        var input = new bool[] { true, true, false, false, true };

        var bitReader = new BitReader(input);

        bitReader.Read(3).ToArray().Should().BeEquivalentTo(new[] { true, true, false });
        bitReader.Read(2).ToArray().Should().BeEquivalentTo(new[] { false, true });
    }

    [Fact]
    public void BitReader_ReadAsInt()
    {
        var input = new bool[] { true, true, false, false, true };

        var bitReader = new BitReader(input);

        bitReader.ReadAsNumber(4).Should().Be(12);
    }
}
