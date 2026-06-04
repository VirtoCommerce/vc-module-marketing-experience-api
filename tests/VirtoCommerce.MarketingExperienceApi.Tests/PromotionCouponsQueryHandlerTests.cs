using System.Threading;
using System.Threading.Tasks;
using VirtoCommerce.MarketingExperienceApi.Data.Queries;
using VirtoCommerce.MarketingModule.Core.Model.Promotions;
using VirtoCommerce.MarketingModule.Core.Model.Promotions.Search;
using VirtoCommerce.MarketingModule.Core.Search;
using Xunit;

namespace VirtoCommerce.MarketingExperienceApi.Tests;

[Trait("Category", "Unit")]
public class PromotionCouponsQueryHandlerTests
{
    // Hand-written fake (no Moq dependency in this test project) that captures the
    // PromotionSearchCriteria the handler builds. SearchNoCloneAsync(criteria) delegates
    // to ISearchService<,,>.SearchAsync(criteria, clone: false), so capturing there is
    // equivalent to capturing what the handler passes to SearchNoCloneAsync.
    private sealed class CapturingPromotionSearchService : IPromotionSearchService
    {
        public PromotionSearchCriteria CapturedCriteria { get; private set; }

        public Task<PromotionSearchResult> SearchAsync(PromotionSearchCriteria criteria, bool clone = true)
        {
            CapturedCriteria = criteria;
            return Task.FromResult(new PromotionSearchResult());
        }

        public Task<PromotionSearchResult> SearchPromotionsAsync(PromotionSearchCriteria criteria)
        {
            CapturedCriteria = criteria;
            return Task.FromResult(new PromotionSearchResult());
        }
    }

    [Fact]
    public async Task Handle_PassesSortArgumentOntoSearchCriteria()
    {
        // Arrange
        var service = new CapturingPromotionSearchService();
        var handler = new PromotionCouponsQueryHandler(service);
        var query = new PromotionCouponsQuery
        {
            StoreId = "TestStore",
            Sort = "endDate:asc",
            Skip = 0,
            Take = 20,
        };

        // Act
        await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(service.CapturedCriteria);
        Assert.Equal("endDate:asc", service.CapturedCriteria.Sort);
    }
}
