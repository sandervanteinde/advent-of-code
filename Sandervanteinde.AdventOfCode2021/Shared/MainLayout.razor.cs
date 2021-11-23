namespace Sandervanteinde.AdventOfCode2021.Shared;

public partial class MainLayout
{
    public bool IsDrawerOpen { get; set; } = true;

    public void ToggleOpen()
    {
        IsDrawerOpen = !IsDrawerOpen;
    }
}
