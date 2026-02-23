using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using VirtoCommerce.MarketingModule.Core.Model.Promotions.Search;
using VirtoCommerce.Xapi.Core.BaseQueries;
using VirtoCommerce.Xapi.Core.Extensions;

namespace VirtoCommerce.MarketingExperienceApi.Data.Queries;

public class PromotionCouponsQuery : SearchQuery<PromotionSearchResult>
{
    public string StoreId { get; set; }
    public string UserId { get; set; }
    public string OrganizationId { get; set; }
    public string CurrencyCode { get; set; }
    public string CultureName { get; set; }

    public override IEnumerable<QueryArgument> GetArguments()
    {
        foreach (var argument in base.GetArguments())
        {
            yield return argument;
        }

        yield return Argument<NonNullGraphType<StringGraphType>>(nameof(StoreId));
        yield return Argument<StringGraphType>(nameof(UserId));
        yield return Argument<StringGraphType>(nameof(CurrencyCode));
        yield return Argument<StringGraphType>(nameof(CultureName));
    }

    public override void Map(IResolveFieldContext context)
    {
        base.Map(context);

        StoreId = context.GetArgument<string>(nameof(StoreId));
        UserId = context.GetArgument<string>(nameof(UserId));
        CurrencyCode = context.GetArgument<string>(nameof(CurrencyCode));
        CultureName = context.GetArgument<string>(nameof(CultureName));
        OrganizationId = context.GetCurrentOrganizationId();
    }
}
