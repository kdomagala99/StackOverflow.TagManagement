using Swashbuckle.AspNetCore.Filters;
using System.Text.Json;

namespace StackOverflow.TagManagement.Api.Examples
{
    public class TagServiceRefreshExample : IExamplesProvider<JsonDocument>
    {
        public JsonDocument GetExamples() => JsonDocument.Parse(@"{
  ""data"": ""Successfully refreshed tags from StackOverflow"",
  ""error"": null
}");
    }
}
