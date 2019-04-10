using flightMate.DataAccess.Abstraction;
using flightMate.DataAccess.Mongo;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;

public static class IdentityServerBuilderExtensions{
    public static IIdentityServerBuilder AddMongoRepository(this IIdentityServerBuilder builder,string connectionString,string database)
    {
        builder.Services.AddTransient<IRepository>((x)=>new MongoRepository(connectionString,database));
        return builder;
    }

    /// <summary>
    /// Configure ClientId / Secrets
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configurationOption"></param>
    /// <returns></returns>
    public static IIdentityServerBuilder AddClients(this IIdentityServerBuilder builder)
    {
        builder.Services.AddTransient<IClientStore, ClientStore>();
        builder.Services.AddTransient<ICorsPolicyService, InMemoryCorsPolicyService>();
        return builder;
    }

    /// <summary>
    /// Configure API & Resources
    /// Note: Api's have also to be configured for clients as part of allowed scope for a given clientID 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IIdentityServerBuilder AddIdentityApiResources(this IIdentityServerBuilder builder)
    {
        builder.Services.AddTransient<IResourceStore, ResourceStore>();

        return builder;
    }

     /// <summary>
    /// Configure Grants
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns></returns>
    public static IIdentityServerBuilder AddPersistedGrants(this IIdentityServerBuilder builder)
    {
        builder.Services.AddSingleton<IPersistedGrantStore, GrantStore>();

        return builder;
    }

}