namespace StackOverflow.TagManagement.Api.Services;

public interface ITagsService
{
    Task GetTagsFromStackOverflowAsync(uint startPage, uint pageCount, CancellationToken cancellationToken);
}
