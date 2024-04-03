using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace StackOverflow.TagManagement.Api.DTO;

public record StackOverflowTagDto
{
    public IEnumerable<CollectiveDto>? Collectives { get; init; }
    public int Count { get; init; }
    [JsonPropertyName("has_synonyms")]
    public bool HasSynonyms { get; init; }
    [JsonPropertyName("is_moderator_only")]
    public bool IsModeratorOnly { get; init; }
    [JsonPropertyName("is_required")]
    public bool IsRequired { get; init; }
    [JsonPropertyName("last_activity_date")]
    public DateTime? ListActivityDate { get; init; }
    [BsonId]
    public required string Name { get; init; }
    public IEnumerable<string>? Synonyms { get; init; }
    [JsonPropertyName("user_id")]
    public int? UserId { get; init; }
}
