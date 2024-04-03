﻿using StackOverflow.TagManagement.Api.DTO;
using System.Numerics;

namespace StackOverflow.TagManagement.Api.Database;

public interface IDbContext
{
    Task<IEnumerable<StackOverflowTagDto>> GetStackOverflowTagsAsync(int skip, int take, CancellationToken cancellationToken);
    Task<StackOverflowTagDto?> GetStackOverflowTagAsync(string name, CancellationToken cancellationToken);
    Task<bool> PostStackOverflowTagAsync(StackOverflowTagDto stackOverflowTag, CancellationToken cancellationToken);
    Task<bool> PutStackOverflowTagAsync(string name, StackOverflowTagDto stackOverflowTag, CancellationToken cancellationToken);
    Task<bool> DeleteStackOverflowTagAsync(string name, CancellationToken cancellationToken);
    Task<long> CountTagsAsync(CancellationToken cancellationToken);
    Task<bool> PostStackOverflowTagsAsync(IEnumerable<StackOverflowTagDto> stackOverflowTags, CancellationToken cancellationToken);
    Task<BigInteger> GetStackOverflowTagsTotalCountAsync(CancellationToken cancellationToken);
}
