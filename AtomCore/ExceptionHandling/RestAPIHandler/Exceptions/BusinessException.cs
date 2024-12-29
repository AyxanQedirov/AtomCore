using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.CCC.ExceptionHandling.RestAPIHandler.Exceptions;

public class BusinessException : BaseException
{
    public BusinessException(string message) : base(message)
    {
        TraceId = Guid.NewGuid().ToString();
    }
}

