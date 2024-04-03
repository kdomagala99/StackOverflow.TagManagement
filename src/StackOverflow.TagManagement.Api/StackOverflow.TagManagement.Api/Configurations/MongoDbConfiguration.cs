namespace StackOverflow.TagManagement.Api.Configurations;

public record MongoDbConfiguration
{
    public string ConnectionString { get; init; } = "mongodb://stackoverflow-tagmanagement-db:27017";
    public string DatabaseName { get; init; } = "StackOverflowManagement";
    public string TagsCollectionName { get; init; } = "Tags";
    public string MetaDataCollectionName { get; init; } = "MetaData";
}
