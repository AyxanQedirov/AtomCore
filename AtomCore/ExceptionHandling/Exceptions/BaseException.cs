namespace AtomCore.ExceptionHandling.Exceptions;

public class BaseException : Exception
{
    public string TraceId { get; set; }

    public BaseException(string message) : base(message)
    {

    }
}
