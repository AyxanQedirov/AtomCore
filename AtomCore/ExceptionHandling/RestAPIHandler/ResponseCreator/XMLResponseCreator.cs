using AtomCore.ExceptionHandling.Exceptions;
using AtomCore.ExceptionHandling.RestAPIHandler.ProblemDetails;
using Microsoft.AspNetCore.Http;

namespace AtomCore.ExceptionHandling.RestAPIHandler.ResponseCreator;

internal class XMLResponseCreator(IHttpContextAccessor _httpContextAccessor) : IResponseCreator
{
    public string GetContentType()
        => "application/xml";

    public async Task HandleException(BusinessException exception)
    {
        BusinessExceptionProblemDetial problemDetail = new(exception);

        _httpContextAccessor.HttpContext.Response.ContentType = GetContentType();
        _httpContextAccessor.HttpContext.Response.StatusCode = problemDetail.StatusCode;
        await _httpContextAccessor.HttpContext.Response.WriteAsync(problemDetail.ToXMLString());
    }

    public async Task HandleException(Exception exception)
    {
        UnknownExceptionProblemDetail problemDetail = new(exception);

        _httpContextAccessor.HttpContext.Response.ContentType = GetContentType();
        _httpContextAccessor.HttpContext.Response.StatusCode = problemDetail.StatusCode;
        await _httpContextAccessor.HttpContext.Response.WriteAsync(problemDetail.ToXMLString());
    }

    public async Task HandleException(ValidationException exception)
    {
        ValidationExceptionProblemDetail problemDetail = new(exception);

        _httpContextAccessor.HttpContext.Response.ContentType = GetContentType();
        _httpContextAccessor.HttpContext.Response.StatusCode = problemDetail.StatusCode;
        await _httpContextAccessor.HttpContext.Response.WriteAsync(problemDetail.ToXMLString());
    }
}
