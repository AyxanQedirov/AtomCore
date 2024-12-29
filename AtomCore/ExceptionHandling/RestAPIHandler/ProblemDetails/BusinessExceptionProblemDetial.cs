using AtomCore.CCC.ExceptionHandling.RestAPIHandler.Exceptions;
using System.Net;

namespace AtomCore.CCC.ExceptionHandling.RestAPIHandler.ProblemDetails;

public class BusinessExceptionProblemDetial:BaseProblemDetail
{
    public BusinessExceptionProblemDetial()
    {
        
    }
    public BusinessExceptionProblemDetial(BusinessException exception)
    {
        Type = exception.GetType().Name;
        StatusCode = (int)HttpStatusCode.BadRequest;
        Message = exception.Message;
        TraceId= exception.TraceId;
    }
}
