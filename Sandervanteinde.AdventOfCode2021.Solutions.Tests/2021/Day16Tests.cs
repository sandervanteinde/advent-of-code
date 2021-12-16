using FluentAssertions;
using Sandervanteinde.AdventOfCode2021.Solutions._2021;
using Sandervanteinde.AdventOfCode2021.Solutions.Utils;
using System.Linq;
using Xunit;

namespace Sandervanteinde.AdventOfCode2021.Solutions.Tests._2021;

public class Day16Tests
{
    private readonly FileReader _reader;
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

    [Fact]
    public void ToBitStream_ReturnsCorrectBits()
    {
        var input = new FileReader("D2FE28");
        var bits = Day16.ToBitStream(input);

        var inputToBoolArray = "110100101111111000101000".Select(c => c == '1').ToArray();

        bits.Should().BeEquivalentTo(inputToBoolArray);
    }

    [Fact]
    public void BitReader_ReadsCorrectBits()
    {
        var input = new bool[] { true, true, false, false, true };

        var bitReader = new Day16.BitReader(input);

        bitReader.Read(3).ToArray().Should().BeEquivalentTo(new[] { true, true, false });
        bitReader.Read(2).ToArray().Should().BeEquivalentTo(new[] { false, true });
    }

    [Fact]
    public void BitReader_ReadAsInt()
    {
        var input = new bool[] { true, true, false, false, true };

        var bitReader = new Day16.BitReader(input);

        bitReader.ReadAsNumber(4).Should().Be(12);
    }
}
