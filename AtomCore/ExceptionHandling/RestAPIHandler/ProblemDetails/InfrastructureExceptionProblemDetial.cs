using System.Net;
using AtomCore.ExceptionHandling.Exceptions;

namespace AtomCore.ExceptionHandling.RestAPIHandler.ProblemDetails;

public class InfrastructureExceptionProblemDetial : BaseProblemDetail
{
    public InfrastructureExceptionProblemDetial()
    {
        
    }

    public InfrastructureExceptionProblemDetial(InfrastructureException exception)
    {
        Type = exception.GetType().Name;
        StatusCode = (int)HttpStatusCode.FailedDependency;
        Message=exception.Message;
        TraceId = exception.TraceId;
    }
}