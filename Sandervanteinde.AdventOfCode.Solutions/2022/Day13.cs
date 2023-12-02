using System.Text.Json;
using System.Text.Json.Nodes;

namespace Sandervanteinde.AdventOfCode.Solutions._2022;

internal class Day13 : BaseSolution
{
    public Day13()
        : base("Distress Signal", year: 2022, day: 13)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var input = ParseInput(reader);
        var sum = 0;

        for (var i = 0; i < input.Length; i++)
        {
            var pairIndex = i + 1;
            var (left, right) = input[i];

            if (IsLeftSmallerThanRight(left, right) == Result.Left)
            {
                sum += pairIndex;
            }
        }

        return sum;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var inputAsEnumerable = ParseInput(reader)
            .SelectMany(item => new[] { item.left, item.right });
        var list = inputAsEnumerable.ToList();

        var dividerPacketOne = new JsonArray { new JsonArray { JsonValue.Create(value: 2) } };
        var dividerpacketTwo = new JsonArray { new JsonArray { JsonValue.Create(value: 6) } };
        list.Add(dividerPacketOne);
        list.Add(dividerpacketTwo);

        list.Sort(new Comparer());

        var packteOneIndex = list.IndexOf(dividerPacketOne) + 1;
        var packetTwoIndex = list.IndexOf(dividerpacketTwo) + 1;

        return packetTwoIndex * packteOneIndex;
    }

    private Result IsLeftSmallerThanRight(JsonArray left, JsonArray right)
    {
        for (var i = 0; i < left.Count; i++)
        {
            if (right.Count == i)
            {
                return Result.Right;
            }

            JsonNode? leftItem = left[i], rightItem = right[i];

            var result = (leftItem, rightItem) switch
            {
                (JsonValue leftValue, JsonValue rightValue) => CompareJsonValues(leftValue, rightValue),
                (JsonArray innerleft, JsonArray innerRight) => IsLeftSmallerThanRight(innerleft, innerRight),
                (JsonArray innerLeft, JsonValue rightValue) => IsLeftSmallerThanRight(innerLeft, AsArray(rightValue)),
                (JsonValue leftValue, JsonArray innerRight) => IsLeftSmallerThanRight(AsArray(leftValue), innerRight),
                _ => throw new NotSupportedException("unknown item pair found.")
            };

            if (result != Result.Equal)
            {
                return result;
            }
        }

        return Result.Left;

        static JsonArray AsArray(JsonValue value)
        {
            return new JsonArray() { JsonValue.Create(value.GetValue<int>()) };
        }

        static Result CompareJsonValues(JsonValue left, JsonValue right)
        {
            var leftValue = left.GetValue<int>();
            var rightValue = right.GetValue<int>();
            return leftValue == rightValue
                ? Result.Equal
                : leftValue < rightValue
                    ? Result.Left
                    : Result.Right;
        }
    }

    private static (JsonArray left, JsonArray right)[] ParseInput(FileReader reader)
    {
        var result = new List<(JsonArray, JsonArray)>();
        var enumerator = reader.ReadAsSpanLineByLine();

        while (enumerator.MoveNext())
        {
            var line1 = ParseNode(enumerator.Current);
            enumerator.MoveNext();
            var line2 = ParseNode(enumerator.Current);
            enumerator.MoveNext();
            result.Add((line1, line2));
        }

        return result.ToArray();

        static JsonArray ParseNode(ReadOnlySpan<char> line)
        {
            return JsonSerializer.Deserialize<JsonArray>(line)
                ?? throw new InvalidOperationException("Invalid input.");
        }
    }

    private enum Result { Left = -1, Equal = 0, Right = 1 }

    internal class Comparer : IComparer<JsonArray>
    {
        public int Compare(JsonArray? x, JsonArray? y)
        {
            if (x is null || y is null)
            {
                throw new InvalidOperationException("Null values are not allowed.");
            }

            var result = IsLeftSmallerThanRight(x, y);
            return (int)result;
        }

        private Result IsLeftSmallerThanRight(JsonArray left, JsonArray right)
        {
            if (left.Count == 0 && right.Count == 0)
            {
                return Result.Equal;
            }

            for (var i = 0; i < left.Count; i++)
            {
                if (right.Count == i)
                {
                    return Result.Right;
                }

                JsonNode? leftItem = left[i], rightItem = right[i];

                var result = (leftItem, rightItem) switch
                {
                    (JsonValue leftValue, JsonValue rightValue) => CompareJsonValues(leftValue, rightValue),
                    (JsonArray innerleft, JsonArray innerRight) => IsLeftSmallerThanRight(innerleft, innerRight),
                    (JsonArray innerLeft, JsonValue rightValue) => IsLeftSmallerThanRight(innerLeft, AsArray(rightValue)),
                    (JsonValue leftValue, JsonArray innerRight) => IsLeftSmallerThanRight(AsArray(leftValue), innerRight),
                    _ => throw new NotSupportedException("unknown item pair found.")
                };

                if (result != Result.Equal)
                {
                    return result;
                }
            }

            return left.Count == right.Count
                ? Result.Equal
                : Result.Left;

            static JsonArray AsArray(JsonValue value)
            {
                return new JsonArray() { JsonValue.Create(value.GetValue<int>()) };
            }

            static Result CompareJsonValues(JsonValue left, JsonValue right)
            {
                var leftValue = left.GetValue<int>();
                var rightValue = right.GetValue<int>();
                return leftValue == rightValue
                    ? Result.Equal
                    : leftValue < rightValue
                        ? Result.Left
                        : Result.Right;
            }
        }
    }
}
