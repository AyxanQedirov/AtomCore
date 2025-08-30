using AtomCore.ExceptionHandling.Exceptions;
using AtomCore.ExceptionHandling.RestAPIHandler.ResponseCreator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AtomCore.ExceptionHandling.RestAPIHandler;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IResponseCreator _responseCreator;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    public GlobalExceptionHandlerMiddleware(RequestDelegate next, IResponseCreator responseCreator, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _responseCreator = responseCreator;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            var handler = FindHandler(ex);
            await handler;
        }
    }

    public Task FindHandler(Exception ex)
    {
        if (ex is BusinessException businessException) return _responseCreator.HandleException(businessException);
        if(ex is ValidationException validationException) return _responseCreator.HandleException(validationException);
        if(ex is AuthenticationException authenticationException) return _responseCreator.HandleException(authenticationException);
        if(ex is AuthorizationException authorizationException) return _responseCreator.HandleException(authorizationException);
        if(ex is InfrastructureException infrastructureException) return _responseCreator.HandleException(infrastructureException);
        if(ex is RateLimitException rateLimitException) return _responseCreator.HandleException(rateLimitException);

        return _responseCreator.HandleException(ex);
    }
}
