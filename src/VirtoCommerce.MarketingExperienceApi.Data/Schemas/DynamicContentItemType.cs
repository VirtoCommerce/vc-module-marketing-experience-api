using GraphQL.Types;
using VirtoCommerce.MarketingModule.Core.Model;
using VirtoCommerce.Xapi.Core.Extensions;
using VirtoCommerce.Xapi.Core.Helpers;
using VirtoCommerce.Xapi.Core.Schemas;
using VirtoCommerce.Xapi.Core.Services;

namespace VirtoCommerce.MarketingExperienceApi.Data.Schemas
{
    public class DynamicContentItemType : ExtendableGraphType<DynamicContentItem>
    {
        public DynamicContentItemType(IDynamicPropertyResolverService dynamicPropertyResolverService)
        {
            Field(x => x.Id);
            Field(x => x.ContentType);
            Field(x => x.Name);
            Field(x => x.Description);
            Field(x => x.Priority);

            ExtendableFieldAsync<ListGraphType<DynamicPropertyValueType>>(
            "dynamicProperties",
            "Dynamic content dynamic property values",
                QueryArgumentPresets.GetArgumentForDynamicProperties(),
                async context => await dynamicPropertyResolverService.LoadDynamicPropertyValues(context.Source, context.GetArgumentOrValue<string>("cultureName")));
        }
    }
}
