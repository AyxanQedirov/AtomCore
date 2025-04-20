using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace AtomCore.RateLimiting;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddIPRateLimiter(this IServiceCollection services,
        ConfigurationOptions redisOptions)
    {
        redisOptions.AbortOnConnectFail = false;
        
        services.AddKeyedSingleton<IConnectionMultiplexer>("ForRateLimiter",
            ConnectionMultiplexer.Connect(redisOptions));
        services.AddHttpContextAccessor();
        
        return services;
    }

    public static IServiceCollection AddIPRateLimitPipeline(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RateLimitByIPPipeline<,>));
        return services;
    }
}