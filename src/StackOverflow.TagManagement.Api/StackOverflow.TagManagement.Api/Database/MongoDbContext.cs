using MongoDB.Driver;
using StackOverflow.TagManagement.Api.Configurations;
using StackOverflow.TagManagement.Api.DTO;
using StackOverflow.TagManagement.Api.Models;
using System.Numerics;

namespace StackOverflow.TagManagement.Api.Database;

public class MongoDbContext : IDbContext
{
    private readonly IMongoCollection<StackOverflowTagDto> stackOverflowTagsCollection;
    private readonly IMongoCollection<TagMetaData> metaDataCollection;

    public MongoDbContext(MongoDbConfiguration mongoDbConfiguration)
    {
        var client = new MongoClient(mongoDbConfiguration.ConnectionString);
        var database = client.GetDatabase(mongoDbConfiguration.DatabaseName);
        this.stackOverflowTagsCollection = database.GetCollection<StackOverflowTagDto>(mongoDbConfiguration.TagsCollectionName);
        this.metaDataCollection = database.GetCollection<TagMetaData>(mongoDbConfiguration.MetaDataCollectionName);
    }

    public async Task<bool> DeleteStackOverflowTagAsync(string name, CancellationToken cancellationToken = default)
    {
        var tag = await this.stackOverflowTagsCollection.Find(tag => tag.Name.Equals(name))
            .FirstOrDefaultAsync(cancellationToken);

        if (tag is null)
        {
            return false;
        }

        var result = await this.stackOverflowTagsCollection.DeleteOneAsync(tag => tag.Name.Equals(name), cancellationToken);
        var deleteResult = result.DeletedCount > 0;

        if (deleteResult)
        {
            await this.AddCountValue(-tag.Count);
        }

        return deleteResult;
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
            await this.AddCountValue(stackOverflowTag.Count);
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

        if (result != null)
        {
            await this.AddCountValue(result.Count);
            return true;
        }

        return false;
    }
    public async Task<long> CountTagsAsync(CancellationToken cancellationToken = default)
        => await this.stackOverflowTagsCollection.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);

    public async Task<bool> PostStackOverflowTagsAsync(IEnumerable<StackOverflowTagDto> stackOverflowTags, CancellationToken cancellationToken)
    {
        try
        {
            await this.stackOverflowTagsCollection.InsertManyAsync(stackOverflowTags, cancellationToken: cancellationToken);
            await this.AddCountValue(stackOverflowTags.Sum(tag => tag.Count));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<BigInteger> GetStackOverflowTagsTotalCountAsync(CancellationToken cancellationToken)
        => (await this.metaDataCollection.FindAsync(md => md.Version.Equals(Constants.METADATA_VERSION))).First()?.TotalTagCount ?? BigInteger.Zero;

    private async Task AddCountValue(BigInteger value)
    {
        if ((await this.metaDataCollection.FindAsync(md => md.Version.Equals(Constants.METADATA_VERSION))).Any())
        {
            await this.metaDataCollection.UpdateOneAsync(md => md.Version.Equals(Constants.METADATA_VERSION),
                               Builders<TagMetaData>.Update.Inc(md => md.TotalTagCount, value));
        }
        else
        {
            await this.metaDataCollection.InsertOneAsync(new TagMetaData(value));
        }
    }
}
