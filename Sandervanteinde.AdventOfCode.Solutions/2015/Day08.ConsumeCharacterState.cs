namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day08
{
    private class ConsumeCharacterState : IState
    {
        public void Next(StringParser parser, OpMode mode)
        {
            var c = parser.ConsumeNextChar();
            if (c == '\\')
            {
                switch (mode)
                {
                    case OpMode.Read:
                        parser.SetState<UnknownEscapeState>();
                        return;
                    case OpMode.Write:
                        parser.AddToOutput(c, c);
                        return;
                }
            }

            if (c == '"')
            {
                if (mode == OpMode.Write)
                {
                    parser.AddToOutput('\\', c);
                }
                if (!parser.HasNext)
                {
                    parser.EndString();
                }
                return;
            }
            parser.AddToOutput(c);
        }
    }
}
