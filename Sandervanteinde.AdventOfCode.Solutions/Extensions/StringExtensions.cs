﻿namespace Sandervanteinde.AdventOfCode.Solutions.Extensions;

public static class StringExtensions
{
    public static bool IsPalindrome(this string s)
    {
        var span = s.AsSpan();
        var firstIndex = 0;
        var lastIndex = ^1;

        while (span.Length > 1)
        {
            if (span[firstIndex] != span[lastIndex])
            {
                return false;
            }

            span = span.Slice(start: 1, span.Length - 2);
        }

        return true;
    }
}
