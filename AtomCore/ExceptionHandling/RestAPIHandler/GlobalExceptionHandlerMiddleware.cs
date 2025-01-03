using AtomCore.ExceptionHandling.Exceptions;
using AtomCore.ExceptionHandling.RestAPIHandler.ResponseCreator;
using Microsoft.AspNetCore.Http;

namespace AtomCore.ExceptionHandling.RestAPIHandler;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IResponseCreator _responseCreator;
    public GlobalExceptionHandlerMiddleware(RequestDelegate next, IResponseCreator responseCreator)
    {
        _next = next;
        _responseCreator = responseCreator;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
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

        return _responseCreator.HandleException(ex);
    }
}
