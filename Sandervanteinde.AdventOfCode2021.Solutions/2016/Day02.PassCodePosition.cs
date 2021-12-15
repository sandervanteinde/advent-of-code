namespace Sandervanteinde.AdventOfCode2021.Solutions._2016;

internal partial class Day02
{
    private class PassCodePosition
    {
        public int X { get; private set; } = -2;
        public int Y { get; private set; } = 0;

        public PassCodePosition MoveUp()
        {
            Y--;
            if (Math.Abs(Y) + Math.Abs(X) > 2)
            {
                Y++;
            }
            return this;
        }

        public PassCodePosition MoveDown()
        {
            Y++;
            if (Math.Abs(Y) + Math.Abs(X) > 2)
            {
                Y--;
            }
            return this;
        }

        public PassCodePosition MoveLeft()
        {
            X--;
            if (Math.Abs(Y) + Math.Abs(X) > 2)
            {
                X++;
            }
            return this;

        }

        public PassCodePosition MoveRight()
        {

            X++;
            if (Math.Abs(Y) + Math.Abs(X) > 2)
            {
                X--;
            }
            return this;
        }

        public char GetPasscodeChar()
        {
            return this switch
            {
                { X: 0, Y: -2 } => '1',
                { X: -1, Y: -1 } => '2',
                { X: 0, Y: -1 } => '3',
                { X: 1, Y: -1 } => '4',
                { X: -2, Y: 0 } => '5',
                { X: -1, Y: 0 } => '6',
                { X: 0, Y: 0 } => '7',
                { X: 1, Y: 0 } => '8',
                { X: 2, Y: 0 } => '9',
                { X: -1, Y: 1 } => 'A',
                { X: 0, Y: 1 } => 'B',
                { X: 1, Y: 1 } => 'C',
                { X: 0, Y: 2 } => 'D',
                _ => throw new NotSupportedException("Invalid location of passcode")
            };
        }
    }
}
