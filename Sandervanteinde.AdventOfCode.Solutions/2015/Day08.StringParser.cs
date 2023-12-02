using System.Text;

namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day08
{
    private class StringParser
    {
        private readonly string input;
        private readonly OpMode mode;

        private readonly StringBuilder sb = new();
        private bool finalized;
        private int index;

        private IState state = new ExpectStartStringState();

        public StringParser(string input, OpMode mode)
        {
            this.input = input;
            this.mode = mode;
        }

        public bool HasNext => input.Length > index;

        internal void SetState<T>()
            where T : IState, new()
        {
            state = new T();
        }

        internal void EndString()
        {
            finalized = true;
        }

        internal char ConsumeNextChar()
        {
            return input[index++];
        }

        internal void AddToOutput(params char[] chars)
        {
            foreach (var c in chars)
            {
                sb.Append(c);
            }
        }

        internal string GetResult()
        {
            if (mode is OpMode.Write)
            {
                sb.Append(value: '"');
            }

            while (!finalized)
            {
                state.Next(this, mode);
            }

            if (HasNext)
            {
                throw new InvalidOperationException("The string got a \" but it was not the last character of the string.");
            }

            if (mode is OpMode.Write)
            {
                sb.Append(value: '"');
            }

            return sb.ToString();
        }
    }
}
