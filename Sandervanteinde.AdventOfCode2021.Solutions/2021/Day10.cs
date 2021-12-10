namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day10 : BaseSolution
{
    public Day10()
        : base("Syntax Scoring", 2021, 10)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var invalidChars = new Dictionary<char, int>
        {
            { '}', 0 },
            { ')', 0 },
            { ']', 0 },
            { '>', 0 }
        };

        foreach (var line in reader.ReadLineByLine())
        {
            var stack = new Stack<char>();
            var shouldContinue = true;
            var i = 0;
            while (shouldContinue && i < line.Length)
            {
                var c = line[i];
                switch (c)
                {
                    case '{':
                    case '[':
                    case '<':
                    case '(':
                        stack.Push(c);
                        break;
                    case '}':
                    case ']':
                    case '>':
                    case ')':
                        AttemptRemoveFromStack(c);
                        break;
                    default:
                        throw new NotSupportedException("Invalid input character found: " + c);
                }
                i++;
            }

            void AttemptRemoveFromStack(char c)
            {
                if (!RemoveFromStack(stack, OppositeOf(c), out var invalidChar))
                {
                    shouldContinue = false;
                    invalidChars[c]++;
                }
            }
        }

        return invalidChars[')'] * 3 + invalidChars[']'] * 57 + invalidChars['}'] * 1197 + invalidChars['>'] * 25137;
    }

    private static char OppositeOf(char c)
    {
        return c switch
        {
            '(' => ')',
            ')' => '(',
            '[' => ']',
            ']' => '[',
            '<' => '>',
            '>' => '<',
            '{' => '}',
            '}' => '{',
            _ => throw new NotSupportedException()
        };
    }

    private static bool RemoveFromStack(Stack<char> stack, char c, out char invalidChar)
    {
        var result = stack.Pop();
        if (result != c)
        {
            invalidChar = result;
            return false;
        }
        invalidChar = '\0';
        return true;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var autoCompleteCharsValues = new Dictionary<char, int>
        {
            { '(', 1 },
            { '[', 2 },
            { '{', 3 },
            { '<', 4 }
        };
        var scores = new List<long>();
        foreach (var line in reader.ReadLineByLine())
        {
            var stack = new Stack<char>();
            var shouldContinue = true;
            var i = 0;
            while (shouldContinue && i < line.Length)
            {
                var c = line[i];
                switch (c)
                {
                    case '{':
                    case '[':
                    case '<':
                    case '(':
                        stack.Push(c);
                        break;
                    case '}':
                    case ']':
                    case '>':
                    case ')':
                        AttemptRemoveFromStack(c);
                        break;
                    default:
                        throw new NotSupportedException("Invalid input character found: " + c);
                }
                i++;
            }

            if (!shouldContinue)
            {
                // invalid line
                continue;
            }

            var score = 0L;

            while (stack.Count > 0)
            {
                score *= 5;
                var c = stack.Pop();
                score += autoCompleteCharsValues[c];
            }

            scores.Add(score);

            void AttemptRemoveFromStack(char c)
            {
                if (!RemoveFromStack(stack, OppositeOf(c), out var invalidChar))
                {
                    shouldContinue = false;
                }
            }
        }

        return scores
            .OrderBy(x => x)
            .Skip(scores.Count / 2)
            .First();
    }
}
