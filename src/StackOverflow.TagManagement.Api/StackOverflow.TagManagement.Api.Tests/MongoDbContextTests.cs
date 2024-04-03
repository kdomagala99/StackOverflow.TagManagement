using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StackOverflow.TagManagement.Api.Configurations;
using StackOverflow.TagManagement.Api.Database;

namespace StackOverflow.TagManagement.Api.Tests
{
    [TestClass]
    public class MongoDbContextTests
    {
        private string connectionString;
        private string databaseName;

        [TestInitialize]
        public void Initialize()
        {
            this.connectionString = "mongodb://localhost:27017";
            this.databaseName = $"test_db{Guid.NewGuid()}";
        }

        [TestMethod]
        public void MongoCreateDatabase_ShouldCreate_ThenDrop()
        {
            this.dbContext = new MongoDbContext(new MongoDbConfiguration() { ConnectionString = this.connectionString, DatabaseName = this.databaseName });
            var client = new MongoClient(this.connectionString);
            client.DropDatabase(this.databaseName);
        }
    }
}
