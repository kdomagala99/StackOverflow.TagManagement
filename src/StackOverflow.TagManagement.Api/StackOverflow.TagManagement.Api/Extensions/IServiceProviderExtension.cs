using StackOverflow.TagManagement.Api.Database;
using StackOverflow.TagManagement.Api.Services;

namespace StackOverflow.TagManagement.Api.Extensions;

public static class IServiceProviderExtension
{
    public static async Task<IServiceProvider> GetFirstTags(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        var db = services.GetRequiredService<IDbContext>();
        var tagsService = services.GetRequiredService<ITagsService>();
        int i = 1;

        // Using MAX_ITERATIONS prevents infinite loop
        while (await db.CountTagsAsync(CancellationToken.None) < Constants.MIN_TAGS_COUNT && i < Constants.MAX_ITERATIONS)
        {
            var count = (int)Math.Ceiling((Constants.MIN_TAGS_COUNT - (double)await db.CountTagsAsync(CancellationToken.None))/99.0);
            await tagsService.GetTagsFromStackOverflowAsync(i, count, CancellationToken.None);
            i++;
        }

        return serviceProvider;
    }
}
