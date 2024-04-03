using System.Text.Json.Serialization;

namespace StackOverflow.TagManagement.Api.DTO;

public record CollectiveDto
{
    public string? Description { get; init; }
    [JsonPropertyName("external_links")]
    public IEnumerable<CollectiveExternalLinkDto>? ExternalLinks { get; init; }
    public string? Link { get; init; }
    public string? Name { get; init; }
    public string? Slug { get; init; }
    public IEnumerable<string>? Tags { get; init; }
}
