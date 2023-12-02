using System.Collections.Generic;
using FluentAssertions;
using Sandervanteinde.AdventOfCode.Solutions._2021;
using Sandervanteinde.AdventOfCode.Solutions.Utils;
using Xunit;

namespace Sandervanteinde.AdventOfCode.Solutions.Tests._2021;

public class Day14Tests
{
    private readonly FileReader _reader;
    private readonly Day14 _sut;

    public Day14Tests()
    {
        _reader = new FileReader(
            @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C"
        );

        _sut = new Day14();
    }

    [Fact]
    public void StepOne_ShouldWorkWithExample()
    {
        _sut.DetermineStepOneResult(_reader)
            .Should()
            .Be(expected: 1588);
    }

    [Fact]
    public void StepTwo_ShouldWorkWithExample()
    {
        _sut.DetermineStepTwoResult(_reader)
            .Should()
            .Be(expected: 2_188_189_693_529);
    }

    [Fact]
    public void AddCountsToDictionary_ShouldWork()
    {
        var rule = new Dictionary<string, char>
        {
            { "AA", 'A' },
            { "AB", 'C' },
            { "AC", 'A' },
            { "BA", 'B' },
            { "BB", 'C' },
            { "BC", 'A' },
            { "CA", 'B' },
            { "CB", 'C' },
            { "CC", 'A' }
        };
        var key = new Day14.LookupKey { Key = "AB", Index = 1 };
        var result = _sut.AddCountsToDictionary(rule, new Dictionary<Day14.LookupKey, Dictionary<char, ulong>>(), in key, maxIndex: 3);
        result.Should()
            .BeEquivalentTo(new Dictionary<char, ulong> { [key: 'A'] = 4, [key: 'C'] = 3 });
    }
}
