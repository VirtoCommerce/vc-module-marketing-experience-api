using GraphQL.Types;
using VirtoCommerce.Xapi.Core.Schemas;
using VirtoCommerce.MarketingModule.Core.Model.DynamicContent;

namespace VirtoCommerce.MarketingExperienceApi.Data.Schemas
{
    public class EvaluateDynamicContentResultType : ExtendableGraphType<EvaluateDynamicContentResult>
    {
        public EvaluateDynamicContentResultType()
        {
            Field(x => x.TotalCount);

            ExtendableField<ListGraphType<DynamicContentItemType>>(nameof(EvaluateDynamicContentResult.Items), resolve: context => context.Source.Items);
        }
    }
}
