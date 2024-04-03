using StackOverflow.TagManagement.Api.DTO;

namespace StackOverflow.TagManagement.Api.Database;

public interface IDbContext
{
    Task<IEnumerable<StackOverflowTagDto>> GetStackOverflowTagsAsync(CancellationToken cancellationToken);
    Task<StackOverflowTagDto?> GetStackOverflowTagAsync(string name, CancellationToken cancellationToken);
    Task<bool> PostStackOverflowTagAsync(StackOverflowTagDto stackOverflowTag, CancellationToken cancellationToken);
    Task<bool> PutStackOverflowTagAsync(string name, StackOverflowTagDto stackOverflowTag, CancellationToken cancellationToken);
    Task<bool> DeleteStackOverflowTagAsync(string name, CancellationToken cancellationToken);
    Task<long> CountTagsAsync(CancellationToken cancellationToken);
    Task<bool> PostStackOverflowTagsAsync(IEnumerable<StackOverflowTagDto> stackOverflowTags, CancellationToken cancellationToken);
    Task<int> GetStackOverflowTagsTotalCountAsync(CancellationToken cancellationToken);
}
