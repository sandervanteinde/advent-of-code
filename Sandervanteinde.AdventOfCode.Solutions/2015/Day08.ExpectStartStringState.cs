namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day08
{
    private class ExpectStartStringState : IState
    {
        public void Next(StringParser parser, OpMode mode)
        {
            if (!parser.HasNext)
            {
                throw new NotSupportedException();
            }

            var c = parser.ConsumeNextChar();

            if (c != '"')
            {
                throw new NotImplementedException();
            }

            if (mode is OpMode.Write)
            {
                parser.AddToOutput('\\', c);
            }

            parser.SetState<ConsumeCharacterState>();
        }
    }
}
