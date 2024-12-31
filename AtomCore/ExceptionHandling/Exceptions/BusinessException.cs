using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.ExceptionHandling.Exceptions;

public class BusinessException : BaseException
{
    public BusinessException(string message) : base(message)
    {
        TraceId = Guid.NewGuid().ToString();
    }
}

public class ValidationException : BaseException
{
    public ValidationException(string message) : base(message)
    {
        TraceId = Guid.NewGuid().ToString();
    }
}

