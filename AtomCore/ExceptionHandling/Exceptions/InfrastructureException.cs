namespace AtomCore.ExceptionHandling.Exceptions;

public class InfrastructureException : BaseException
{
    public InfrastructureException(string message) : base(message)
    {
        TraceId = Guid.NewGuid().ToString();
    }

    public InfrastructureException(string message, string traceId) : base(message)
    {
        TraceId = traceId;
    }
}