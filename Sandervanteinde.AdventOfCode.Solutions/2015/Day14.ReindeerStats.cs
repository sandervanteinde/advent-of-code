namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day14
{
    private class ReindeerStats
    {
        public string ReindeerName { get; init; } = null!;
        public int SpeedInKms { get; init; }
        public int FlyDurationInSeconds { get; init; }
        public int RestTimeInSeconds { get; init; }
        public int CycleTime { get; init; }

        public override string ToString()
        {
            return $"{ReindeerName} can fly {SpeedInKms} km/s for {FlyDurationInSeconds} seconds, but then must rest for {RestTimeInSeconds} seconds.";
        }

        public int DistanceTravelledAfter(int seconds)
        {
            var positionInLastCycle = seconds % CycleTime;
            var cyclesFlown = seconds / CycleTime;
            var kmsFlownInCycles = cyclesFlown * FlyDurationInSeconds * SpeedInKms;

            var secondsFlownInLastCycle =
                positionInLastCycle > FlyDurationInSeconds
                    ? FlyDurationInSeconds
                    : positionInLastCycle;

            return kmsFlownInCycles + (secondsFlownInLastCycle * SpeedInKms);
        }
    }
}
