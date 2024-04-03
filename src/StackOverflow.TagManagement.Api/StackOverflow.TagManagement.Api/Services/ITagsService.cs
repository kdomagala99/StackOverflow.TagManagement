using StackOverflow.TagManagement.Api.DTO;

namespace StackOverflow.TagManagement.Api.Services;

public interface ITagsService
{
    Task GetTagsFromStackOverflowAsync(int startPage, int pageCount, CancellationToken cancellationToken);
    Task<IEnumerable<GetStackOverflowTagDto>> GetTagsFromLocalDbAsync(int skip, int take, Order orderByName, Order orderByTagPercentage, CancellationToken cancellationToken);
}
