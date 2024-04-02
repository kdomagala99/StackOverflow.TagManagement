using StackOverflow.TagManagement.Api.Models;

namespace StackOverflow.TagManagement.Api.Database;

public interface IDbContext
{
    Task<IEnumerable<StackOverflowTag>> GetStackOverflowTagsAsync(int skip, int take, CancellationToken cancellationToken);
    Task<StackOverflowTag?> GetStackOverflowTagAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> PostStackOverflowTagAsync(StackOverflowTag stackOverflowTag, CancellationToken cancellationToken);
    Task<bool> PutStackOverflowTagAsync(Guid id, StackOverflowTag stackOverflowTag, CancellationToken cancellationToken);
    Task<bool> DeleteStackOverflowTagAsync(Guid id, CancellationToken cancellationToken);
}
