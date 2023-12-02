namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day23
{
    private class IncrementInstruction : IInstruction
    {
        private readonly char register;

        public IncrementInstruction(char register)
        {
            if (register is not 'a' and not 'b')
            {
                throw new InvalidOperationException("Register should be A or B");
            }

            this.register = register;
        }

        public void PerformInstruction(Computer computer)
        {
            switch (register)
            {
                case 'a':
                    computer.RegisterA++;
                    break;
                case 'b':
                    computer.RegisterB++;
                    break;
            }
        }
    }
}
