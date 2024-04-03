using Microsoft.Extensions.Logging;
using NSubstitute;
using StackOverflow.TagManagement.Api.Database;
using StackOverflow.TagManagement.Api.Services;

namespace StackOverflow.TagManagement.Api.Tests
{
    [TestClass]
    public class TagsServiceTests
    {
        private TagsService sut;
        private IHttpClientFactory httpClientFactory = Substitute.For<IHttpClientFactory>();
        private IDbContext dbContext = Substitute.For<IDbContext>();
        private ILogger<TagsService> logger = Substitute.For<ILogger<TagsService>>();

        [TestInitialize]
        public void Initialize()
        {
            this.sut = new TagsService(this.httpClientFactory, this.dbContext, this.logger);
            this.dbContext.GetStackOverflowTagsAsync(Arg.Any<CancellationToken>())
                .Returns(FakeResponses.GetStackOverflowTagsAsyncResponse());
            this.dbContext.GetStackOverflowTagsTotalCountAsync(Arg.Any<CancellationToken>())
                .Returns(10);
        }

        [TestMethod]
        public async Task GetTagsFromLocalDbAsync_WhenCalled_ShouldReturnTags()
        {
            var skip = 0;
            var take = 10;
            var orderByName = Order.Asc;
            var orderByTagPercentage = Order.Desc;
            var cancellationToken = CancellationToken.None;

            var result = await this.sut.GetTagsFromLocalDbAsync(skip, take, orderByName, orderByTagPercentage, cancellationToken);

            Assert.IsNotNull(result);
        }
    }
}
