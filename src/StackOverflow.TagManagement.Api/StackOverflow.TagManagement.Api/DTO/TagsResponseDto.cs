namespace StackOverflow.TagManagement.Api.DTO;

public record TagsResponseDto
{
    public required IEnumerable<StackOverflowTagDto> Items { get; init; }
}
