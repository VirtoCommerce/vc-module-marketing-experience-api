using AutoMapper;
using GraphQL.Server;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.ExperienceApiModule.Core.Extensions;
using VirtoCommerce.ExperienceApiModule.Core.Infrastructure;
using VirtoCommerce.MarketingExperienceApi.Data;
using VirtoCommerce.Platform.Core.Modularity;

namespace VirtoCommerce.MarketingExperienceApi.Web;

public class Module : IModule, IHasConfiguration
{
    public ManifestModuleInfo ModuleInfo { get; set; }
    public IConfiguration Configuration { get; set; }

    public void Initialize(IServiceCollection serviceCollection)
    {
        var assemblyMarker = typeof(AssemblyMarker);
        var graphQlBuilder = new CustomGraphQLBuilder(serviceCollection);
        graphQlBuilder.AddGraphTypes(assemblyMarker);
        serviceCollection.AddMediatR(assemblyMarker);
        serviceCollection.AddAutoMapper(assemblyMarker);
        serviceCollection.AddSchemaBuilders(assemblyMarker);
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
