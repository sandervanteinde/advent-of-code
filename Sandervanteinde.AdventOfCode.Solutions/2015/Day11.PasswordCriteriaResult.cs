namespace Sandervanteinde.AdventOfCode.Solutions._2015;

internal partial class Day11
{
    private struct PasswordCriteriaResult
    {
        public int? RequirementTwoFaultIndex { get; init; }
        public bool MeetsRequirementTwo { get; init; }
        public bool IsSuccess { get; init; }
    }
}
