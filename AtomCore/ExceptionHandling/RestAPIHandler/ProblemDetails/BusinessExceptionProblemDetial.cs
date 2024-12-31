using AtomCore.ExceptionHandling.Exceptions;
using System.Net;

namespace AtomCore.ExceptionHandling.RestAPIHandler.ProblemDetails;

public class BusinessExceptionProblemDetial : BaseProblemDetail
{
    public BusinessExceptionProblemDetial()
    {

    }
    public BusinessExceptionProblemDetial(BusinessException exception)
    {
        Type = exception.GetType().Name;
        StatusCode = (int)HttpStatusCode.BadRequest;
        Message = exception.Message;
        TraceId = exception.TraceId;
    }
}

public class ValidationExceptionProblemDetail : BaseProblemDetail
{
    public ValidationExceptionProblemDetail()
    {

    }

    public ValidationExceptionProblemDetail(ValidationException exception)
    {

    }
}
