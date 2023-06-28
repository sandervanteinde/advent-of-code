using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2022;

internal partial class Day16 : BaseSolution
{
    [GeneratedRegex(@"Valve ([A-Z]{2}) has flow rate=(\d+); tunnels? leads? to valves? ([A-Z, ]+)")]
    public static partial Regex ValveRegex();

    public Day16()
        : base("", 2022, 16)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var valves = ParseInput(reader);
        var state = new State(valves);

        var startValve = valves.First(valve => valve.Identifier == "AA");
        var maxFlowRate = int.MinValue;

        AddNextOptions(startValve);

        return maxFlowRate;

        void AddNextOptions(Valve valve, int minute = 1, int currentFlowRate = 0, int totalFlow = 0)
        {
            state.Minute = minute;
            state.TargetValve = valve;
            state.CurrentFlowRate = currentFlowRate;
            state.TotalFlow = totalFlow;
            if(minute > 30)
            {
                maxFlowRate = Math.Max(totalFlow, maxFlowRate);
                return;
            }

            if (state.HasVisitedBefore())
            {
                return;
            }

            if (valve is { FlowRate: > 0 } and { IsOpen: false })
            {
                valve.IsOpen = true;
                AddNextOptions(valve, minute + 1, currentFlowRate + valve.FlowRate, totalFlow + currentFlowRate);
                valve.IsOpen = false;
            }

            foreach (var leadsTo in valve.LeadsToValves)
            {
                AddNextOptions(leadsTo, minute + 1, currentFlowRate, totalFlow + currentFlowRate);
            }


        }
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        throw new NotImplementedException();
    }

    private static IReadOnlyList<Valve> ParseInput(FileReader reader)
    {
        var regex = ValveRegex();
        var result = new Dictionary<string, Valve>();
        foreach (var line in reader.ReadLineByLine())
        {
            var match = regex.Match(line);
            if (match is not { Success: true, Groups: [_, { Value: string sourceValve }, { Value: string flowRateAsString }, _] })
            {
                throw new NotSupportedException($"The line {line} was not correctly formatted.");
            }

            var valve = new Valve
            {
                FlowRate = int.Parse(flowRateAsString),
                Identifier = sourceValve
            };
            result.Add(valve.Identifier, valve);
        }
        foreach (var line in reader.ReadLineByLine())
        {
            var match = regex.Match(line);
            if (match is not { Success: true, Groups: [_, { Value: string sourceValveIdentifier }, _, { Value: string leadsToAsfullString }] })
            {
                throw new NotSupportedException($"The line {line} was not correctly formatted.");
            }

            var sourceValve = result[sourceValveIdentifier];
            foreach(var targetValve in leadsToAsfullString.Split(", "))
            {
                sourceValve.AddLeadsTo(result[targetValve]);
            }
        }

        return result.Values.ToList().AsReadOnly();
    }

    private class State
    {
        private readonly HashSet<int> _visited = new();
        public IReadOnlyList<Valve> Valves { get; }
        public int Minute { get; internal set; }
        public int CurrentFlowRate { get; set; }
        public int TotalFlow { get; set; }
        public Valve TargetValve { get; internal set; }

        public State(IReadOnlyList<Valve> valves)
        {
            Valves = valves;
        }

        [DebuggerStepThrough]
        public override int GetHashCode()
        {
            var hash = new HashCode();
            for(var i = 0; i < Valves.Count; i++)
            {
                hash.Add(Valves[i].IsOpen);
            }
            hash.Add(Minute);
            hash.Add(TargetValve.Identifier);
            hash.Add(CurrentFlowRate);
            hash.Add(TotalFlow);
            return hash.ToHashCode();
        }

        [DebuggerStepThrough]
        internal bool HasVisitedBefore()
        {
            return !_visited.Add(GetHashCode());
        }
    }

    private class Valve
    {
        private readonly List<Valve> _leadsToValves = new();
        public required string Identifier { get; init; }
        public required int FlowRate { get; init; }

        public bool IsOpen { get; set; }

        public IReadOnlyList<Valve> LeadsToValves { get; }

        public Valve()
        {
            LeadsToValves = _leadsToValves.AsReadOnly();
        }

        public void AddLeadsTo(Valve valve)
        {
            _leadsToValves.Add(valve);
        }
    }
}
