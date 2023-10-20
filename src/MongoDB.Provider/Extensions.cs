using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MongoDB.Provider;

public static class Extensions
{
    public static IServiceCollection AddMongoDB<TMongoDBContext>(this IServiceCollection services, IConfiguration configuration)
        where TMongoDBContext : MongoDBContext
    {
        services.Configure<MongoDBSetting>(configuration.GetSection(nameof(MongoDBSetting)));
        services.AddTransient<TMongoDBContext>();

        return services;
    }
}
