namespace Sandervanteinde.AdventOfCode.Solutions.ProjectEuler;

public interface IProjectEulerSolution
{
    string Title { get; }
    int Id { get; }

    object GetSolution();
}
