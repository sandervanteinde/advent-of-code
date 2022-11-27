namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day06
{
    public class LanternFish
    {
        public int State { get; }
        public int TurnMade { get; }
        public LanternFish(int initialSate, int turnMade)
        {
            State = initialSate;
            TurnMade = turnMade;
        }
    }
}
