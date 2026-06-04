using System.Threading;
using System.Threading.Tasks;
using VirtoCommerce.MarketingModule.Core.Model.Promotions.Search;
using VirtoCommerce.MarketingModule.Core.Search;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Xapi.Core.Infrastructure;

namespace VirtoCommerce.MarketingExperienceApi.Data.Queries;

public class PromotionCouponsQueryHandler : IQueryHandler<PromotionCouponsQuery, PromotionSearchResult>
{
    private readonly IPromotionSearchService _promotionSearchService;

    public PromotionCouponsQueryHandler(IPromotionSearchService promotionSearchService)
    {
        _promotionSearchService = promotionSearchService;
    }

    public async Task<PromotionSearchResult> Handle(PromotionCouponsQuery request, CancellationToken cancellationToken)
    {
        var promotionSearchCriteria = GetPromotionSearchCriteria(request);
        var searchResult = await _promotionSearchService.SearchNoCloneAsync(promotionSearchCriteria);
        return searchResult;
    }

    protected virtual PromotionSearchCriteria GetPromotionSearchCriteria(PromotionCouponsQuery request)
    {
        var promotionSearchCriteria = AbstractTypeFactory<PromotionSearchCriteria>.TryCreateInstance();

        promotionSearchCriteria.OnlyActive = true;
        promotionSearchCriteria.IsPublic = true;
        promotionSearchCriteria.Store = request.StoreId;
        promotionSearchCriteria.CouponCount = 1;
        promotionSearchCriteria.Skip = request.Skip;
        promotionSearchCriteria.Take = request.Take;
        promotionSearchCriteria.Sort = request.Sort;

        return promotionSearchCriteria;
    }
}
