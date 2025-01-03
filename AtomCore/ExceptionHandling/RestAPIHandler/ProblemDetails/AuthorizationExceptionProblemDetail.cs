using AtomCore.ExceptionHandling.Exceptions;
using System.Net;

namespace AtomCore.ExceptionHandling.RestAPIHandler.ProblemDetails;

public class AuthorizationExceptionProblemDetail : BaseProblemDetail
{
    public AuthorizationExceptionProblemDetail()
    {
        
    }

    public AuthorizationExceptionProblemDetail(AuthorizationException exception)
    {
        Type = exception.GetType().Name;
        StatusCode = (int)HttpStatusCode.Forbidden;
        Message = exception.Message;
        TraceId = Guid.NewGuid().ToString();
    }
}
