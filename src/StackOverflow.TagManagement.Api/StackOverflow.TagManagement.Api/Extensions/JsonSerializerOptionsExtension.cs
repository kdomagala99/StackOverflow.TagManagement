using System.Text.Json;
using System.Text.Json.Serialization;

namespace StackOverflow.TagManagement.Api.Extensions;

public static class JsonSerializerOptionsExtension
{
    private static JsonSerializerOptions ReadOptionsObject => new()
    {
        PropertyNameCaseInsensitive = true,
    };

    private static JsonSerializerOptions WriteOptionsObject => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    public static JsonSerializerOptions ReadOptions => ReadOptionsObject;
    public static JsonSerializerOptions WriteOptions => WriteOptionsObject;
}
