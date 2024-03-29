﻿namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal class Day01 : BaseSolution
{
    public Day01()
        : base("Not Quite List", year: 2015, day: 1)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var asLookup = reader.ReadCharByChar()
            .ToLookup(c => c);
        var leftBracketCount = asLookup[key: '(']
            .Count();
        var rightBracketCount = asLookup[key: ')']
            .Count();
        return leftBracketCount - rightBracketCount;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var currentFloor = 0;

        for (var i = 0; i < reader.Input.Length; i++)
        {
            var value = reader.Input[i];
            currentFloor = value switch
            {
                '(' => currentFloor + 1,
                ')' => currentFloor - 1,
                _ => throw new NotSupportedException()
            };

            if (currentFloor == -1)
            {
                return i + 1;
            }
        }

        throw new InvalidOperationException("Santa never reached the basement");
    }
}
