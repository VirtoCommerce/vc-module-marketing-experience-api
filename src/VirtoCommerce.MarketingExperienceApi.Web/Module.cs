using GraphQL;
using GraphQL.MicrosoftDI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.MarketingExperienceApi.Data;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Xapi.Core.Extensions;

namespace VirtoCommerce.MarketingExperienceApi.Web;

public class Module : IModule, IHasConfiguration
{
    public ManifestModuleInfo ModuleInfo { get; set; }
    public IConfiguration Configuration { get; set; }

    public void Initialize(IServiceCollection serviceCollection)
    {
        _ = new GraphQLBuilder(serviceCollection, builder =>
        {
            builder.AddSchema(serviceCollection, typeof(AssemblyMarker));
        });
    }

    public void PostInitialize(IApplicationBuilder appBuilder)
    {
        // Method intentionally left empty.
    }

    public void Uninstall()
    {
        // Method intentionally left empty.
    }
}
