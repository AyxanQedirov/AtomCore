namespace AtomCore.ExceptionHandling.Exceptions;

public class AuthorizationException : BaseException
{
    public AuthorizationException(string message) : base(message) { }
}