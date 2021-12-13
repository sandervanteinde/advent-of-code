namespace Sandervanteinde.AdventOfCode2021.Solutions.ProjectEuler;

public interface IProjectEulerSolution
{
    string Title { get; }
    int Id { get; }

    object GetSolution();
}
