using MongoDB.Driver;
using StackOverflow.TagManagement.Api.Configurations;
using StackOverflow.TagManagement.Api.DTO;
using StackOverflow.TagManagement.Api.Models;

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

    public async Task<IEnumerable<StackOverflowTagDto>> GetStackOverflowTagsAsync(CancellationToken cancellationToken = default)
    {
        var result = await this.stackOverflowTagsCollection.Find(_ => true)
            .ToListAsync(cancellationToken);
        return result;
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

    public async Task<int> GetStackOverflowTagsTotalCountAsync(CancellationToken cancellationToken)
        => (await this.metaDataCollection.FindAsync(md => md.Version.Equals(Constants.METADATA_VERSION))).First()?.TotalTagCount ?? 0;

    private async Task AddCountValue(int value)
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
