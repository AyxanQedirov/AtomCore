using AtomCore.ExceptionHandling.RestAPIHandler.ProblemDetails;

ValidationExceptionProblemDetail problemDetail = new()
{
    Type = "ValidationException",
    StatusCode = 422,
    Message = "Salam",
    TraceId = Guid.NewGuid().ToString(),
    Errors = new()
    {
        {"Salam",["1","2","3"] }
    }
};

Console.WriteLine(problemDetail.ToJsonString());