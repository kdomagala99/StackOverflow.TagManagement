namespace StackOverflow.TagManagement.Api;

public static class Constants
{
    private const string StackOverflowBaseUrl = "https://api.stackexchange.com/2.3";

    public static class StackOverflow
    {
        public static string GetTagsEndpoint(uint page = 1,
            uint pageSize = 100,
            Order order = Order.Asc,
            uint min = 1,
            uint max = 999,
            Sort sort = Sort.Popular)
            => $@"{StackOverflowBaseUrl}/tags?page={page}&pagesize={pageSize}&fromdate=1217541600&todate=1893452400&order={order.ToString().ToLower()}&min={min}&max={max}&sort={sort.ToString().ToLower()}&site=stackoverflow";
    }

    public static class Messages
    {
        public const string SuccessfullyRefreshedTags = "Successfully refreshed tags from StackOverflow";
    }

    public const uint MAX_ITERATIONS = 100;
    public const uint MIN_TAGS_COUNT = 1000;
}

public enum Order 
{
    Asc,
    Desc
}

public enum Sort
{
    Popular,
    Activity,
    Name
}
