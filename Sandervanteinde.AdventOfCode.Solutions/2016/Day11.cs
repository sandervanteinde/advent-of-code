namespace Sandervanteinde.AdventOfCode.Solutions._2016;

public class Day11 : BaseSolution
{
    public Day11()
        : base("Radioisotope Thermoelectric Generators", year: 2016, day: 11)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        return "Unknown";
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        return "Unknown";
    }

    private object ParseFloor(string line, int index)
    {
        var floor = new Floor { FloorNumber = index + 1 };

        foreach (var machineType in AllMachineTypes())
        {
            if (line.Contains(machineType.Item1))
            {
                floor.AddMachine(new Machine((Type)machineType.Item2, (MachineType)machineType.Item3));
            }
        }

        return floor;
    }

    private List<(string, object, object)> AllMachineTypes()
    {
        return Enum.GetValues<Type>()
            .SelectMany(
                type => Enum.GetValues<MachineType>()
                    .Select(
                        machineType =>
                        {
                            return
                            (
                                $"{type.ToString().ToLower()}{(machineType == MachineType.Generator ? " generator" : "-compatible microchip")}",
                                type as object,
                                machineType as object
                            );
                        }
                    )
            )
            .ToList();
    }
}

file enum Type
{
    Thulium,
    Plutonium,
    Strontium,
    Promethium,
    Ruthenium
}

file enum MachineType
{
    Microchip,
    Generator
}

file record struct Machine(Type Type, MachineType MachineType);

file class Office
{
    public required Floor One { get; init; }
    public required Floor Two { get; init; }
    public required Floor Three { get; init; }
    public required Floor Four { get; init; }
}

file class Floor
{
    private readonly List<Machine> _machines = new();
    public required int FloorNumber { get; init; }

    public void AddMachine(Machine machine)
    {
        _machines.Add(machine);
    }

    public override int GetHashCode()
    {
        var hashcode = new HashCode();
        hashcode.Add(FloorNumber);

        foreach (var machine in _machines)
        {
            hashcode.Add(machine);
        }

        return hashcode.ToHashCode();
    }
}
