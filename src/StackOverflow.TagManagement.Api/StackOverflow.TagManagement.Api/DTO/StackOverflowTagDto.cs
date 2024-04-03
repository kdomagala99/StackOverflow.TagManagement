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
    public string? Name { get; init; }
    public IEnumerable<string>? Synonyms { get; init; }
    [JsonPropertyName("user_id")]
    public int? UserId { get; init; }

    public StackOverflowTagDto()
    {
    }

    public StackOverflowTagDto(IEnumerable<CollectiveDto>? collectives, int count, bool hasSynonyms, bool isModeratorOnly, bool isRequired, DateTime? listActivityDate, string name, IEnumerable<string>? synonyms, int? userId)
    {
        this.Collectives = collectives;
        this.Count = count;
        this.HasSynonyms = hasSynonyms;
        this.IsModeratorOnly = isModeratorOnly;
        this.IsRequired = isRequired;
        this.ListActivityDate = listActivityDate;
        this.Name = name;
        this.Synonyms = synonyms;
        this.UserId = userId;
    }

    public StackOverflowTagDto(StackOverflowTagDto other)
    {
        this.Collectives = other.Collectives;
        this.Count = other.Count;
        this.HasSynonyms = other.HasSynonyms;
        this.IsModeratorOnly = other.IsModeratorOnly;
        this.IsRequired = other.IsRequired;
        this.ListActivityDate = other.ListActivityDate;
        this.Name = other.Name;
        this.Synonyms = other.Synonyms;
        this.UserId = other.UserId;
    }
}
