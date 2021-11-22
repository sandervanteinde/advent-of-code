using Microsoft.AspNetCore.Components;
using Sandervanteinde.AdventOfCode2021.Solutions;

namespace Sandervanteinde.AdventOfCode2021.Shared;

public partial class NavMenu
{
    private class SelectedYear
    {
        public int Year { get; set; }
        public bool IsExpanded { get; set; }

        public IEnumerable<IAdventOfCodeSolution> Solutions { get; set; }
    }

    [Inject]
    public SolutionRegistry Registry { get; set; } = null!;

    private List<SelectedYear> SelectedYears = new();

    protected override void OnInitialized()
    {
        var years = Registry.AvailableYears
            .Select(year => new SelectedYear { Year = year, IsExpanded = false, Solutions = Registry.DaysForYear(year).Select(day => Registry.GetSolution(year, day)).ToList() })
            .ToList();
        var highestYear = years.MaxBy(year => year.Year);
        highestYear.IsExpanded = true;
        SelectedYears = years;
    }
}
