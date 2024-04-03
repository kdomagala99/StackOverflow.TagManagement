using StackOverflow.TagManagement.Api.Database;
using StackOverflow.TagManagement.Api.DTO;
using StackOverflow.TagManagement.Api.Exceptions;
using StackOverflow.TagManagement.Api.Extensions;
using System.Text.Json;

namespace StackOverflow.TagManagement.Api.Services;

public class TagsService(IHttpClientFactory httpClientFactory, IDbContext dbContext) : ITagsService
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient();
    private readonly IDbContext dbContext = dbContext;

    public async Task<IEnumerable<GetStackOverflowTagDto>> GetTagsFromLocalDbAsync(int skip, int take, Order orderByName, Order orderByTagPercentage, CancellationToken cancellationToken)
    {
        var tags = await this.dbContext.GetStackOverflowTagsAsync(cancellationToken);

        if (tags is null)
        {
            throw new NotFoundException();
        }

        var totalCount = await this.dbContext.GetStackOverflowTagsTotalCountAsync(cancellationToken);
        var values = tags.Select(t =>
        {
            var count = (double)t.Count / totalCount;
            return new GetStackOverflowTagDto(count, t);
        });

        if (orderByName != Order.None && orderByTagPercentage != Order.None)
        {
            values = orderByTagPercentage == Order.Asc
                ? orderByName switch
                {
                    Order.Asc => values.OrderBy(v => v.Name).ThenBy(v => v.TagPercentage),
                    Order.Desc => values.OrderByDescending(v => v.Name).ThenBy(v => v.TagPercentage),
                    _ => values
                }
                : orderByName switch
                {
                    Order.Asc => values.OrderBy(v => v.Name).ThenByDescending(v => v.TagPercentage),
                    Order.Desc => values.OrderByDescending(v => v.Name).ThenByDescending(v => v.TagPercentage),
                    _ => values
                };
        }
        else
        {
            values = orderByName switch
            {
                Order.Asc => values.OrderBy(v => v.Name),
                Order.Desc => values.OrderByDescending(v => v.Name),
                _ => values
            };

            values = orderByTagPercentage switch
            {
                Order.Asc => values.OrderBy(v => v.TagPercentage),
                Order.Desc => values.OrderByDescending(v => v.TagPercentage),
                _ => values
            };
        }

        return values.Skip(skip).Take(take);
    }

    public async Task GetTagsFromStackOverflowAsync(int startPage = 1, int pageCount = 20, CancellationToken cancellationToken = default)
    {
        List<StackOverflowTagDto> tags = [];
        for (int i = startPage; i <= startPage + pageCount; i++)
        {
            tags.AddRange(await this.GetTagsAsync(i, cancellationToken));
        }
        
        var result = await this.dbContext.PostStackOverflowTagsAsync(tags, cancellationToken);
        if(!result)
        {
            throw new InsufficientStorageException();
        }
    }

    private async Task<IEnumerable<StackOverflowTagDto>> GetTagsAsync(int page, CancellationToken cancellationToken = default)
    {
        var response = await this.httpClient.GetAsync(Constants.StackOverflow.GetTagsEndpoint(page: page), cancellationToken);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new ServiceUnavailableException();
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var tagsResponse = JsonSerializer.Deserialize<TagsResponseDto>(content, JsonSerializerOptionsExtension.ReadOptions);
        return tagsResponse?.Items ?? [];
    }
}
