using MongoDB.Driver;
using StackOverflow.TagManagement.Api.Configurations;
using StackOverflow.TagManagement.Api.Models;

namespace StackOverflow.TagManagement.Api.Database
{
    public class MongoDbContext : IDbContext
    {
        private readonly IMongoCollection<StackOverflowTag> _stackOverflowTags;

        public MongoDbContext(MongoDbConfiguration mongoDbConfiguration)
        {
            var client = new MongoClient(mongoDbConfiguration.ConnectionString);
            var database = client.GetDatabase(mongoDbConfiguration.DatabaseName);
            _stackOverflowTags = database.GetCollection<StackOverflowTag>(mongoDbConfiguration.CollectionName);
        }

        public async Task<bool> DeleteStackOverflowTagAsync(Guid id, CancellationToken cancellationToken = default)   
        {
            var result = await _stackOverflowTags.DeleteOneAsync(tag => tag.Id.Equals(id), cancellationToken);
            return result.DeletedCount > 0;
        }

        public async Task<StackOverflowTag?> GetStackOverflowTagAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _stackOverflowTags.FindAsync(tag => tag.Id.Equals(id),
                cancellationToken: cancellationToken);
            return result.FirstOrDefault(cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<StackOverflowTag>> GetStackOverflowTagsAsync(int skip = 0, int take = int.MaxValue, CancellationToken cancellationToken = default)
        {
            var result = await _stackOverflowTags.Find(_ => true)
                                                 .Skip(skip)
                                                 .Limit(take)
                                                 .ToListAsync(cancellationToken);
            return result;
        }

        public async Task<bool> PostStackOverflowTagAsync(StackOverflowTag stackOverflowTag, CancellationToken cancellationToken = default)
        {
            try
            {
                await _stackOverflowTags.InsertOneAsync(stackOverflowTag, 
                    cancellationToken: cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> PutStackOverflowTagAsync(Guid id, StackOverflowTag stackOverflowTag, CancellationToken cancellationToken = default)
        {
            var result = await _stackOverflowTags.FindOneAndReplaceAsync(tag => tag.Id.Equals(id),
                stackOverflowTag,
                cancellationToken: cancellationToken);
            return result != null;
        }
    }
}
