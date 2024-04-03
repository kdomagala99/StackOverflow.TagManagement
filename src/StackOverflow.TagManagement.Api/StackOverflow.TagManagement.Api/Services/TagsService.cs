using StackOverflow.TagManagement.Api.Database;
using StackOverflow.TagManagement.Api.DTO;
using StackOverflow.TagManagement.Api.Exceptions;
using StackOverflow.TagManagement.Api.Extensions;
using System.Text.Json;

namespace StackOverflow.TagManagement.Api.Services;

public class TagsService(IHttpClientFactory httpClientFactory, IDbContext dbContext, ILogger<TagsService> logger) : ITagsService
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient();
    private readonly IDbContext dbContext = dbContext;
    private readonly ILogger logger = logger;

    public async Task<IEnumerable<GetStackOverflowTagDto>> GetTagsFromLocalDbAsync(int skip, int take, Order orderByName, Order orderByTagPercentage, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("{UtcTime}: Getting tags from local db...", DateTime.UtcNow);
        var tags = await this.dbContext.GetStackOverflowTagsAsync(cancellationToken) ?? throw new NotFoundException();
        this.logger.LogInformation("{UtcTime}: Finished getting tags from local db.", DateTime.UtcNow);
        this.logger.LogTrace("{UtcTime}: Tags: {Tags}", DateTime.UtcNow, JsonSerializer.Serialize(tags, JsonSerializerOptionsExtension.WriteOptions));

        this.logger.LogInformation("{UtcTime}: Getting tags total count from local db...", DateTime.UtcNow);
        var totalCount = await this.dbContext.GetStackOverflowTagsTotalCountAsync(cancellationToken);
        this.logger.LogInformation("{UtcTime}: Finished getting tags total count from local db.", DateTime.UtcNow);

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
            this.logger.LogInformation("{UtcTime}: Getting tags from StackOverflow for {page} page...", DateTime.UtcNow, i);
            tags.AddRange(await this.GetTagsAsync(i, cancellationToken));
        }
        this.logger.LogInformation("{UtcTime}: Finished getting tags from StackOverflow.", DateTime.UtcNow);
        this.logger.LogTrace("{UtcTime}: Tags: {tags}", DateTime.UtcNow, JsonSerializer.Serialize(tags, JsonSerializerOptionsExtension.WriteOptions));

        this.logger.LogInformation("{UtcTime}: Posting tags to local db...", DateTime.UtcNow);
        var result = await this.dbContext.PostStackOverflowTagsAsync(tags, cancellationToken);
        if(!result)
        {
            throw new InsufficientStorageException();
        }
        this.logger.LogInformation("{UtcTime}: Finished posting tags to local db.", DateTime.UtcNow);
    }

    private async Task<IEnumerable<StackOverflowTagDto>> GetTagsAsync(int page, CancellationToken cancellationToken = default)
    {
        this.logger.LogDebug("{UtcTime}: Getting data from StackOverflow...", DateTime.UtcNow);
        var response = await this.httpClient.GetAsync(Constants.StackOverflow.GetTagsEndpoint(page: page), cancellationToken);
        this.logger.LogDebug("{UtcTime}: Finished getting data from StackOverflow.", DateTime.UtcNow);
        this.logger.LogTrace("{UtcTime}: Response: {Response}", DateTime.UtcNow, JsonSerializer.Serialize(response, JsonSerializerOptionsExtension.WriteOptions));

        if (!response.IsSuccessStatusCode)
        {
            throw new ServiceUnavailableException();
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var tagsResponse = JsonSerializer.Deserialize<TagsResponseDto>(content, JsonSerializerOptionsExtension.ReadOptions);
        return tagsResponse?.Items ?? [];
    }
}
