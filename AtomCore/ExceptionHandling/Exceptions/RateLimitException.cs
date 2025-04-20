namespace AtomCore.ExceptionHandling.Exceptions;

public class RateLimitException : BaseException
{
    public RateLimitException() : base("The maximum number of request was exceeded")
    {
        
    }
}