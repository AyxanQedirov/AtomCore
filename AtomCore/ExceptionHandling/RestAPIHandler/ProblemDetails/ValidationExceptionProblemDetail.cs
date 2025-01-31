﻿using AtomCore.ExceptionHandling.Exceptions;
using System.Net;
using System.Text.Json;

namespace AtomCore.ExceptionHandling.RestAPIHandler.ProblemDetails;

public class ValidationExceptionProblemDetail : BaseProblemDetail
{
    public Dictionary<string, List<string>> Errors { get; set; }
    public ValidationExceptionProblemDetail()
    {

    }

    public ValidationExceptionProblemDetail(ValidationException exception)
    {
        Type=exception.GetType().Name;
        StatusCode= (int)HttpStatusCode.UnprocessableEntity;
        Message = exception.Message;
        TraceId = exception.TraceId;
        Errors = exception.Errors;
    }

    public override string ToJsonString()
    {
        return JsonSerializer.Serialize(this);
    }
}
