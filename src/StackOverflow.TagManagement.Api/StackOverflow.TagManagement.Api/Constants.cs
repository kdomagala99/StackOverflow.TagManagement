namespace StackOverflow.TagManagement.Api;

public static class Constants
{
    private const string StackOverflowBaseUrl = "https://api.stackexchange.com/2.3";

    public static class StackOverflow
    {
        public static string GetTagsEndpoint(int page = 1,
            int pageSize = 100,
            Order order = Order.Desc,
            int min = 1,
            int max = 999,
            Sort sort = Sort.Popular)
            => $@"{StackOverflowBaseUrl}/tags?page={page}&pagesize={pageSize}&fromdate=1217541600&todate=1893452400&order={order.ToString().ToLower()}&min={min}&max={max}&sort={sort.ToString().ToLower()}&site=stackoverflow";
    }

    public static class Messages
    {
        public const string SuccessfullyRefreshedTags = "Successfully refreshed tags from StackOverflow";
    }

    public const int MAX_ITERATIONS = 100;
    public const int MIN_TAGS_COUNT = 1000;

    public const string METADATA_VERSION = "MetaDataV1";
}

public enum Order 
{
    None,
    Asc,
    Desc
}

public enum Sort
{
    Popular,
    Activity,
    Name
}
