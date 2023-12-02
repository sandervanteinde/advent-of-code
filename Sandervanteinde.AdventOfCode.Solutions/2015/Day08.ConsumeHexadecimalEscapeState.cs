using System.Globalization;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day08
{
    private class ConsumeHexadecimalEscapeState : IState
    {
        private char? firstDigit;

        public void Next(StringParser parser, OpMode mode)
        {
            if (mode is OpMode.Write)
            {
                throw new InvalidOperationException();
            }

            var next = parser.ConsumeNextChar();

            if (firstDigit is null)
            {
                firstDigit = next;
                return;
            }

            var hexDigit = $"{firstDigit}{next}";
            var c = (char)int.Parse(hexDigit, NumberStyles.HexNumber);
            parser.AddToOutput(c);
            parser.SetState<ConsumeCharacterState>();
        }
    }
}
