using StackOverflow.TagManagement.Api.DTO;

namespace StackOverflow.TagManagement.Api.Tests;

public static class FakeResponses
{
    public static IEnumerable<StackOverflowTagDto> GetStackOverflowTagsAsyncResponse()
        => new List<StackOverflowTagDto>
        {
            new() { Name = "tag1", Count = 1 },
            new() { Name = "tag2", Count = 2 },
            new() { Name = "tag3", Count = 3 },
            new() { Name = "tag4", Count = 4 },
            new() { Name = "tag5", Count = 5 },
            new() { Name = "tag6", Count = 6 },
            new() { Name = "tag7", Count = 7 },
            new() { Name = "tag8", Count = 8 },
            new() { Name = "tag9", Count = 9 },
            new() { Name = "tag10", Count = 10 },
        };
}
