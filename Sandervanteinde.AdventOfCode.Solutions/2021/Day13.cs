using System.Text;
using System.Text.RegularExpressions;
using Sandervanteinde.AdventOfCode.Solutions;
using Sandervanteinde.AdventOfCode.Solutions.Utils;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day13 : BaseSolution
{
    public Day13()
        : base("Transparent Origami", 2021, 13)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        var (points, folds) = ParseInput(reader);
        var firstFold = folds.First();
        var mutationFunc = GetMutationFunc(firstFold);
        foreach (var p in points)
        {
            if (ShouldMutate(p.Point, firstFold))
            {
                p.ApplyMutation(mutationFunc);
            }
        }

        return points.Select(p => p.Point)
            .Distinct()
            .Count();
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var (points, folds) = ParseInput(reader);
        foreach (var fold in folds)
        {
            var mutationFunc = GetMutationFunc(fold);
            foreach (var p in points)
            {
                if (ShouldMutate(p.Point, fold))
                {
                    p.ApplyMutation(mutationFunc);
                }
            }
        }

        var distinctPoints = points.Select(p => p.Point)
            .ToHashSet();

        var maxX = distinctPoints.Select(p => p.X).Max();
        var maxY = distinctPoints.Select(p => p.Y).Max();

        var sb = new StringBuilder();
        for (var y = 0; y <= maxY; y++)
        {
            for (var x = 0; x <= maxX; x++)
            {
                sb.Append(
                   distinctPoints.Contains(new Point { X = x, Y = y })
                   ? '#'
                   : ' '
                );
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    private bool ShouldMutate(Point point, Fold fold)
    {
        return fold.Axis switch
        {
            Axis.X => point.X > fold.Value,
            Axis.Y => point.Y > fold.Value,
            _ => throw new NotSupportedException()
        };
    }

    private Func<Point, Point> GetMutationFunc(Fold fold)
    {
        var foldValue = fold.Value;
        if (fold.Axis == Axis.X)
        {
            return point => point with { X = fold.Value - (point.X - foldValue) };
        }
        else
        {
            return point => point with { Y = fold.Value - (point.Y - foldValue) };
        }
    }

    private (ICollection<PointWithMutation> points, ICollection<Fold> folds) ParseInput(FileReader reader)
    {
        var points = new List<PointWithMutation>();
        var folds = new List<Fold>();
        var pointMatch = new Regex(@"(\d+),(\d+)");
        var foldMatch = new Regex(@"fold along (x|y)=(\d+)");

        foreach (var line in reader.ReadLineByLine())
        {
            var match = pointMatch.Match(line);
            if (match.Success)
            {
                var point = new Point { X = int.Parse(match.Groups[1].Value), Y = int.Parse(match.Groups[2].Value) };
                points.Add(new(point));
                continue;
            }
            match = foldMatch.Match(line);
            if (match.Success)
            {
                folds.Add(new Fold { Axis = Enum.Parse<Axis>(match.Groups[1].Value, ignoreCase: true), Value = int.Parse(match.Groups[2].Value) });
                continue;
            }

            throw new InvalidOperationException("Invalid input line detected");
        }

        return (points, folds);
    }
}