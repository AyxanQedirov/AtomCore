using AtomCore.ExceptionHandling.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace AtomCore.RateLimiting;

public class RateLimitByIPPipeline<TRequest, TResponse>(
    [FromKeyedServices("ForRateLimiter")]IConnectionMultiplexer multiplexer,
    IHttpContextAccessor accessor)
    : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var attribute = request!.GetType().GetCustomAttributes(true)
            .FirstOrDefault(a => a.GetType() == typeof(RateLimitByIPAttribute)) as RateLimitByIPAttribute;

        if (attribute is not null)
            await LimitingLogic(attribute, request);

        var response = await next();

        return response;
    }

    private async Task LimitingLogic(RateLimitByIPAttribute attribute, TRequest request)
    {
        string ip = accessor.HttpContext!.Connection.RemoteIpAddress!.ToString() == "::1"
            ? "localhost"
            : accessor.HttpContext!.Connection.RemoteIpAddress!.ToString();
        
        string key =
            $"IPRateLimiting:{request!.GetType().Name}:{ip}";

        var redis = multiplexer.GetDatabase();

        var value = await redis.StringGetAsync(key);

        if (value.IsNull)
        {
            await redis.StringSetAsync(key, attribute.CoinCount, attribute.TimeInterval);
            return;
        }

        if (int.Parse(value!) <= 0)
            throw new RateLimitException();

        await redis.StringDecrementAsync(key, 1);
    }
}