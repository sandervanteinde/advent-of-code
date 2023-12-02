namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day08
{
    private class UnknownEscapeState : IState
    {
        public void Next(StringParser parser, OpMode mode)
        {
            if (mode is OpMode.Write)
            {
                throw new InvalidOperationException();
            }

            var c = parser.ConsumeNextChar();

            if (c is '\\' or '"')
            {
                parser.AddToOutput(c);
                parser.SetState<ConsumeCharacterState>();
                return;
            }

            if (c == 'x')
            {
                parser.SetState<ConsumeHexadecimalEscapeState>();
                return;
            }

            throw new InvalidOperationException("Invalid escape sequence");
        }
    }
}
