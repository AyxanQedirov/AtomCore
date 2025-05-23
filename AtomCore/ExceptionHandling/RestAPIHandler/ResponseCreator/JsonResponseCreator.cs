﻿using AtomCore.ExceptionHandling.Exceptions;
using AtomCore.ExceptionHandling.RestAPIHandler.ProblemDetails;
using Microsoft.AspNetCore.Http;

namespace AtomCore.ExceptionHandling.RestAPIHandler.ResponseCreator;

internal class JsonResponseCreator(IHttpContextAccessor _httpContextAccessor) : IResponseCreator
{
    public string GetContentType()
        => "application/json";

    public async Task HandleException(BusinessException exception)
    {
        BusinessExceptionProblemDetial problemDetail = new(exception);

        _httpContextAccessor.HttpContext!.Response.ContentType = GetContentType();
        _httpContextAccessor.HttpContext.Response.StatusCode = problemDetail.StatusCode;
        await _httpContextAccessor.HttpContext.Response.WriteAsync(problemDetail.ToJsonString());
    }

    public async Task HandleException(InfrastructureException exception)
    {
        InfrastructureExceptionProblemDetial problemDetail = new(exception);
        
        _httpContextAccessor.HttpContext!.Response.ContentType = GetContentType();
        _httpContextAccessor.HttpContext.Response.StatusCode = problemDetail.StatusCode;
        await _httpContextAccessor.HttpContext.Response.WriteAsync(problemDetail.ToJsonString());
    }

    public async Task HandleException(Exception exception)
    {
        UnknownExceptionProblemDetail problemDetail = new(exception);

        _httpContextAccessor.HttpContext!.Response.ContentType = GetContentType();
        _httpContextAccessor.HttpContext.Response.StatusCode = problemDetail.StatusCode;
        await _httpContextAccessor.HttpContext.Response.WriteAsync(problemDetail.ToJsonString());
    }

    public async Task HandleException(ValidationException exception)
    {
        ValidationExceptionProblemDetail problemDetail = new(exception);

        _httpContextAccessor.HttpContext!.Response.ContentType = GetContentType();
        _httpContextAccessor.HttpContext.Response.StatusCode = problemDetail.StatusCode;
        await _httpContextAccessor.HttpContext.Response.WriteAsync(problemDetail.ToJsonString());
    }

    public async Task HandleException(AuthenticationException exception)
    {
        AuthenticationExceptionProblemDetail problemDetail = new(exception);

        _httpContextAccessor.HttpContext!.Response.ContentType = GetContentType();
        _httpContextAccessor.HttpContext.Response.StatusCode = problemDetail.StatusCode;
        await _httpContextAccessor.HttpContext.Response.WriteAsync(problemDetail.ToJsonString());
    }

    public async Task HandleException(AuthorizationException exception)
    {
        AuthorizationExceptionProblemDetail problemDetail = new(exception);

        _httpContextAccessor.HttpContext!.Response.ContentType = GetContentType();
        _httpContextAccessor.HttpContext.Response.StatusCode = problemDetail.StatusCode;
        await _httpContextAccessor.HttpContext.Response.WriteAsync(problemDetail.ToJsonString());
    }
    public async Task HandleException(RateLimitException exception)
    {
        RateLimitingExceptionProblemDetail problemDetail = new(exception);
        
        _httpContextAccessor.HttpContext!.Response.ContentType = GetContentType();
        _httpContextAccessor.HttpContext.Response.StatusCode = problemDetail.StatusCode;
        await _httpContextAccessor.HttpContext.Response.WriteAsync(problemDetail.ToJsonString());
    }
}
