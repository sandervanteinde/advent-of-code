namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;
internal partial class Day11 : BaseSolution
{
    public Day11()
        : base("Corporate Policy", 2015, 11)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        return FindAnswer(reader, false);
    }

    private static string FindAnswer(FileReader reader, bool incrementInitial)
    {
        var chars = reader.ReadCharByChar().ToArray();
        if (incrementInitial)
        {
            IncrementCharAt(^1);
        }
        while (true)
        {
            var criteria = LocationOfFault(chars);
            if (criteria.IsSuccess)
            {
                return new string(chars);
            }

            var faultLocation = criteria.RequirementTwoFaultIndex ?? throw new InvalidOperationException();

            if (!criteria.MeetsRequirementTwo && criteria.RequirementTwoFaultIndex.HasValue)
            {
                IncrementCharAt(faultLocation);
                for (var i = faultLocation + 1; i < chars.Length; i++)
                {
                    chars[i] = 'a';
                }
            }

            IncrementCharAt(^1);
        }

        void PutCharsAtEndAndIncrementPrevious(params char[] newChars)
        {
            IncrementCharAt(new Index(newChars.Length + 1, true));
            Array.Copy(newChars, 0, chars, chars.Length - newChars.Length, newChars.Length);
        }

        void IncrementCharAt(Index index)
        {
            if (chars[index] == 'z')
            {
                IncrementCharAt(new Index(index.IsFromEnd ? index.Value + 1 : index.Value - 1, index.IsFromEnd));
                chars[index] = 'a';
                return;
            }
            chars[index] = (char)(chars[index] + 1);
        }
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var result = FindAnswer(reader, false);
        return FindAnswer(new FileReader(result), true);
    }

    private static PasswordCriteriaResult LocationOfFault(char[] chars)
    {
        var faultLocation = int.MaxValue;

        var meetsCriteriaOne = false;
        var meetsCriteriaTwo = true;
        var meetsCriteriaThree = false;
        char? firstPair = null;


        for (var i = 0; i < chars.Length; i++)
        {
            var c = chars[i];
            if (i >= 2)
            {
                if (chars[i - 2] == c - 2 && chars[i - 1] == c - 1)
                {
                    meetsCriteriaOne = true;
                }
            }

            if (c is 'i' or 'o' or 'l')
            {
                faultLocation = Math.Min(faultLocation, i);
                meetsCriteriaTwo = false;
            }

            if (i >= 1)
            {
                if (chars[i - 1] == c)
                {
                    if (firstPair is null)
                    {
                        firstPair = c;
                    }
                    else if (firstPair != c)
                    {
                        meetsCriteriaThree = true;
                    }
                }
            }
        }

        return new PasswordCriteriaResult
        {
            MeetsRequirementTwo = meetsCriteriaTwo,
            RequirementTwoFaultIndex = faultLocation,
            IsSuccess = meetsCriteriaOne && meetsCriteriaTwo && meetsCriteriaThree
        };

    }
}
