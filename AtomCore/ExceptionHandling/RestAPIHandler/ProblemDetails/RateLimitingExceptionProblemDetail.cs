using System.Net;
using AtomCore.ExceptionHandling.Exceptions;

namespace AtomCore.ExceptionHandling.RestAPIHandler.ProblemDetails;

public class RateLimitingExceptionProblemDetail : BaseProblemDetail
{
    public RateLimitingExceptionProblemDetail()
    {
        
    }

    public RateLimitingExceptionProblemDetail(RateLimitException exception)
    {
        Type= exception.GetType().Name;
        StatusCode= (int)HttpStatusCode.TooManyRequests;
        Message = exception.Message;
        TraceId = Guid.NewGuid().ToString();
    }
}