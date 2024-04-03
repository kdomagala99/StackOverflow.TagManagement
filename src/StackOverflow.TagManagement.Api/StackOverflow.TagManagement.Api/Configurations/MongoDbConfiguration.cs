namespace StackOverflow.TagManagement.Api.Configurations;

public record MongoDbConfiguration
{
    public string ConnectionString { get; init; } = Environment.GetEnvironmentVariable("MongoDb__ConnectionString") ?? "mongodb://stackoverflow-tagmanagement-db:27017";
    public string DatabaseName { get; init; } = Environment.GetEnvironmentVariable("MongoDb__DatabaseName") ?? "StackOverflowManagement";
    public string TagsCollectionName { get; init; } = Environment.GetEnvironmentVariable("MongoDb__TagsCollectionName") ?? "Tags";
    public string MetaDataCollectionName { get; init; } = Environment.GetEnvironmentVariable("MongoDb__MetaDataCollectionName") ?? "MetaData";
}
