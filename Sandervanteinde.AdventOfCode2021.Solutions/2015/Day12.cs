using System.Text.Json;

namespace Sandervanteinde.AdventOfCode2021.Solutions._2015;

internal partial class Day12 : BaseSolution
{
    public Day12()
        : base("JSAbacusFramework.io", 2015, 12)
    {

    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var jsonDoc = reader.DeserializeJsonAs<JsonDocument>() ?? throw new InvalidOperationException();
        var total = 0;
        Iterate(jsonDoc.RootElement);
        return total;

        void Iterate(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Number:
                    total += element.GetInt32();
                    break;
                case JsonValueKind.Array:
                    var arrayLength = element.GetArrayLength();
                    foreach (var arrayItem in element.EnumerateArray())
                    {
                        Iterate(arrayItem);
                    }
                    break;
                case JsonValueKind.Object:
                    foreach (var entry in element.EnumerateObject())
                    {
                        Iterate(entry.Value);
                    }
                    break;
            }
        }
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {

        var jsonDoc = reader.DeserializeJsonAs<JsonDocument>() ?? throw new InvalidOperationException();
        var total = 0;
        Iterate(jsonDoc.RootElement);
        return total;

        void Iterate(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Number:
                    total += element.GetInt32();
                    break;
                case JsonValueKind.Array:
                    var arrayLength = element.GetArrayLength();
                    foreach (var arrayItem in element.EnumerateArray())
                    {
                        Iterate(arrayItem);
                    }
                    break;
                case JsonValueKind.Object:
                    var objects = element.EnumerateObject().ToArray();
                    if (objects.Any(obj => obj.Value.ValueKind == JsonValueKind.String && obj.Value.GetString() == "red"))
                    {
                        return;
                    }
                    foreach (var entry in element.EnumerateObject())
                    {
                        Iterate(entry.Value);
                    }
                    break;
            }
        }
    }
}
