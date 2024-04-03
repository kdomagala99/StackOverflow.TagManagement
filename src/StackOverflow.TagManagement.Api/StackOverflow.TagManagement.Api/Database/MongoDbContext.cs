using MongoDB.Driver;
using StackOverflow.TagManagement.Api.Configurations;
using StackOverflow.TagManagement.Api.DTO;

namespace StackOverflow.TagManagement.Api.Database;

public class MongoDbContext : IDbContext
{
    private readonly IMongoCollection<StackOverflowTagDto> stackOverflowTagsCollection;

    public MongoDbContext(MongoDbConfiguration mongoDbConfiguration)
    {
        var client = new MongoClient(mongoDbConfiguration.ConnectionString);
        var database = client.GetDatabase(mongoDbConfiguration.DatabaseName);
        this.stackOverflowTagsCollection = database.GetCollection<StackOverflowTagDto>(mongoDbConfiguration.CollectionName);
    }

    public async Task<bool> DeleteStackOverflowTagAsync(string name, CancellationToken cancellationToken = default)   
    {
        var result = await this.stackOverflowTagsCollection.DeleteOneAsync(tag => tag.Name.Equals(name), cancellationToken);
        return result.DeletedCount > 0;
    }

    public async Task<StackOverflowTagDto?> GetStackOverflowTagAsync(string name, CancellationToken cancellationToken = default)
    {
        var result = await this.stackOverflowTagsCollection.FindAsync(tag => tag.Name.Equals(name),
            cancellationToken: cancellationToken);
        return result.FirstOrDefault(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<StackOverflowTagDto>> GetStackOverflowTagsAsync(int skip = 0, int take = int.MaxValue, CancellationToken cancellationToken = default)
    {
        var result = await this.stackOverflowTagsCollection.Find(_ => true)
                                             .Skip(skip)
                                             .Limit(take)
                                             .ToListAsync(cancellationToken);
        return result;
    }

    public async Task<bool> PostStackOverflowTagAsync(StackOverflowTagDto stackOverflowTag, CancellationToken cancellationToken = default)
    {
        try
        {
            await this.stackOverflowTagsCollection.InsertOneAsync(stackOverflowTag, 
                cancellationToken: cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> PutStackOverflowTagAsync(string name, StackOverflowTagDto stackOverflowTag, CancellationToken cancellationToken = default)
    {
        var result = await this.stackOverflowTagsCollection.FindOneAndReplaceAsync(tag => tag.Name.Equals(name),
            stackOverflowTag,
            cancellationToken: cancellationToken);
        return result != null;
    }
    public async Task<long> CountTagsAsync(CancellationToken cancellationToken = default)
        => await this.stackOverflowTagsCollection.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);

    public async Task<bool> PostStackOverflowTagsAsync(IEnumerable<StackOverflowTagDto> stackOverflowTags, CancellationToken cancellationToken)
    {
        try
        {
            await this.stackOverflowTagsCollection.InsertManyAsync(stackOverflowTags, cancellationToken: cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
