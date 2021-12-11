using Microsoft.AspNetCore.Components;
using Sandervanteinde.AdventOfCode2021.Solutions;

namespace Sandervanteinde.AdventOfCode2021.Shared;

public partial class MainLayout
{
    [Inject]
    public SolutionRegistry Registry { get; set; } = null!;
    public string[] InitialMenuOpen { get; private set; }

    protected override void OnInitialized()
    {
        InitialMenuOpen = new[] { Registry.AvailableYears.Max().ToString() };
    }
}
