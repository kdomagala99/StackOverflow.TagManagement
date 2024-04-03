using MongoDB.Bson.Serialization.Attributes;

namespace StackOverflow.TagManagement.Api.Models;

public record TagMetaData(int TotalTagCount)
{
    [BsonId]
    public string Version { get; } = Constants.METADATA_VERSION;
}
