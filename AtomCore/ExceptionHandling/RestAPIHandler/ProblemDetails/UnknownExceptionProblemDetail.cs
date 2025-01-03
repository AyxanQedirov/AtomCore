﻿using System.Net;

namespace AtomCore.ExceptionHandling.RestAPIHandler.ProblemDetails;

public class UnknownExceptionProblemDetail : BaseProblemDetail
{
    public UnknownExceptionProblemDetail()
    {

    }
    public UnknownExceptionProblemDetail(Exception exception)
    {
        Type = exception.GetType().Name;
        StatusCode = (int)HttpStatusCode.InternalServerError;
        Message = exception.Message;
        TraceId = Guid.NewGuid().ToString();
    }
}
