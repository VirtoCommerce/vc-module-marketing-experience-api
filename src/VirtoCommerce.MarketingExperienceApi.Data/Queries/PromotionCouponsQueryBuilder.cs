using System.Threading.Tasks;
using GraphQL;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using VirtoCommerce.MarketingExperienceApi.Data.Authorization;
using VirtoCommerce.MarketingExperienceApi.Data.Schemas;
using VirtoCommerce.MarketingModule.Core.Model.Promotions;
using VirtoCommerce.MarketingModule.Core.Model.Promotions.Search;
using VirtoCommerce.Xapi.Core.BaseQueries;
using VirtoCommerce.Xapi.Core.Extensions;
using VirtoCommerce.Xapi.Core.Security.Authorization;

namespace VirtoCommerce.MarketingExperienceApi.Data.Queries;

public class PromotionCouponsQueryBuilder : SearchQueryBuilder<PromotionCouponsQuery, PromotionSearchResult, Promotion, PromotionCouponType>
{
    protected override string Name => "promotionCoupons";

    public PromotionCouponsQueryBuilder(IMediator mediator, IAuthorizationService authorizationService)
        : base(mediator, authorizationService)
    {
    }

    protected override async Task BeforeMediatorSend(IResolveFieldContext<object> context, PromotionCouponsQuery request)
    {
        await base.BeforeMediatorSend(context, request);

        if (!context.IsAuthenticated())
        {
            throw AuthorizationError.AnonymousAccessDenied();
        }

        await Authorize(context, request.StoreId, new PromotionCouponsAuthorizationRequirement());

        context.CopyArgumentsToUserContext();
    }
}
