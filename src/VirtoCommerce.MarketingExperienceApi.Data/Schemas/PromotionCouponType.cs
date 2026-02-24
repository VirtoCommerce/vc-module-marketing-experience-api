using System.Linq;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Resolvers;
using GraphQL.Types;
using VirtoCommerce.MarketingModule.Core.Model.Promotions;
using VirtoCommerce.MarketingModule.Core.Model.Promotions.Search;
using VirtoCommerce.MarketingModule.Core.Search;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Xapi.Core.Extensions;
using VirtoCommerce.Xapi.Core.Schemas;

namespace VirtoCommerce.MarketingExperienceApi.Data.Schemas;

public class PromotionCouponType : ExtendableGraphType<Promotion>
{
    public PromotionCouponType(IDataLoaderContextAccessor dataLoader, ICouponSearchService couponSearchService)
    {
        Field(x => x.Id);
        Field(x => x.EndDate, nullable: true);

        Field<StringGraphType>("name")
            .Resolve(context => GetLocalizedValue(context, context.Source.LocalizedDisplayName, context.Source.Name))
            .Description("Localized name of the promotion.");

        Field<StringGraphType>("description")
            .Resolve(context => GetLocalizedValue(context, context.Source.LocalizedDescription, context.Source.Description))
            .Description("Localized description of the promotion.");

        var couponCodeField = new FieldType
        {
            Name = "couponCode",
            Type = typeof(StringGraphType),
            Resolver = new FuncFieldResolver<Promotion, IDataLoaderResult<string>>(context =>
            {
                var loader = dataLoader.Context.GetOrAddBatchLoader<string, string>("promotion_coupons", async (ids) =>
                {
                    var promotionIds = ids.ToArray();
                    var couponCodeSearchCriteria = new CouponSearchCriteria
                    {
                        PromotionIds = promotionIds,
                        Take = promotionIds.Length,
                    };

                    var searchResult = await couponSearchService.SearchNoCloneAsync(couponCodeSearchCriteria);

                    var result = searchResult.Results
                        .GroupBy(x => x.PromotionId)
                        .ToDictionary(x => x.Key, x => x.FirstOrDefault()?.Code);

                    return result;
                });

                return loader.LoadAsync(context.Source.Id);
            })
        };
        AddField(couponCodeField);
    }

    private static string GetLocalizedValue(IResolveFieldContext context, LocalizedString localizedString, string fallbackValue = null)
    {
        var cultureName = context.GetArgumentOrValue<string>("cultureName");

        if (!string.IsNullOrEmpty(cultureName))
        {
            var localizedValue = localizedString?.GetValue(cultureName);
            if (!string.IsNullOrEmpty(localizedValue))
            {
                return localizedValue;
            }
        }

        return fallbackValue;
    }
}
