namespace AtomCore.ExceptionHandling.Exceptions;

public class ValidationException : BaseException
{
    public Dictionary<string, List<string>> Errors { get; set; }
    public ValidationException(Dictionary<string, List<string>> errors) : base("Validation Exception Occured")
    {
        TraceId = Guid.NewGuid().ToString();
        Errors = errors;
    }
}

