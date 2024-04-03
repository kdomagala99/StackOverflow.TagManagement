using StackOverflow.TagManagement.Api.Database;
using StackOverflow.TagManagement.Api.DTO;
using System.Text.Json;

namespace StackOverflow.TagManagement.Api.Services;

public class TagsService(IHttpClientFactory httpClientFactory, IDbContext dbContext) : ITagsService
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient();
    private readonly IDbContext dbContext = dbContext;

    public async Task GetTagsFromStackOverflowAsync(uint startPage = 1, uint pageCount = 20, CancellationToken cancellationToken = default)
    {
        List<StackOverflowTagDto> tags = [];
        for (uint i = startPage; i <= startPage + pageCount; i++)
        {
            tags.AddRange(await this.GetTagsAsync(i, cancellationToken));
        }
        await this.dbContext.PostStackOverflowTagsAsync(tags, cancellationToken);
    }

    private async Task<IEnumerable<StackOverflowTagDto>> GetTagsAsync(uint page, CancellationToken cancellationToken = default)
    {
        var response = await this.httpClient.GetAsync(Constants.StackOverflow.GetTagsEndpoint(page: page), cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var tagsResponse = JsonSerializer.Deserialize<TagsResponseDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return tagsResponse?.Items ?? [];
    }
}
