using AtomCore.CCC.ExceptionHandling.RestAPIHandler.Exceptions;
using AtomCore.CCC.ExceptionHandling.RestAPIHandler.ProblemDetails;
using Microsoft.AspNetCore.Http;

namespace AtomCore.CCC.ExceptionHandling.RestAPIHandler.ResponseCreator;

internal class JsonResponseCreator(IHttpContextAccessor _httpContextAccessor) : IResponseCreator
{
    public string GetContentType()
        => "application/json";

    public async Task HandleException(BusinessException exception)
    {
        BusinessExceptionProblemDetial problemDetail = new(exception);

        _httpContextAccessor.HttpContext.Response.ContentType=GetContentType();
        _httpContextAccessor.HttpContext.Response.StatusCode=problemDetail.StatusCode;
        await _httpContextAccessor.HttpContext.Response.WriteAsync(problemDetail.ToJsonString());
    }

    public async Task HandleException(Exception exception)
    {
        UnknownExceptionProblemDetail problemDetail = new(exception);

        _httpContextAccessor.HttpContext.Response.ContentType = GetContentType();
        _httpContextAccessor.HttpContext.Response.StatusCode = problemDetail.StatusCode;
        await _httpContextAccessor.HttpContext.Response.WriteAsync(problemDetail.ToJsonString());
    }
}
