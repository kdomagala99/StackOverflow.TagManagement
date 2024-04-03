using MongoDB.Bson.Serialization.Attributes;
using System.Numerics;

namespace StackOverflow.TagManagement.Api.Models;

public record TagMetaData(BigInteger TotalTagCount)
{
    [BsonId]
    public string Version { get; } = Constants.METADATA_VERSION;
}
