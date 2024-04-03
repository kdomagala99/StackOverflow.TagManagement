namespace StackOverflow.TagManagement.Api.Configurations;

public record MongoDbConfiguration
{
    public string ConnectionString { get; init; } = "mongodb://admin:admin@localhost:27017";
    public string DatabaseName { get; init; } = "StackOverflowManagement";
    public string CollectionName { get; init; } = "Tags";
}
