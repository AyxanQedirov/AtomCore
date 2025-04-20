using AtomCore.ExceptionHandling.Exceptions;
using System.Net;

namespace AtomCore.ExceptionHandling.RestAPIHandler.ProblemDetails;

public class AuthenticationExceptionProblemDetail : BaseProblemDetail
{
    public AuthenticationExceptionProblemDetail()
    {
        
    }

    public AuthenticationExceptionProblemDetail(AuthenticationException exception)
    {
        Type= exception.GetType().Name;
        StatusCode= (int)HttpStatusCode.Unauthorized;
        Message = exception.Message;
        TraceId = Guid.NewGuid().ToString();
    }
}