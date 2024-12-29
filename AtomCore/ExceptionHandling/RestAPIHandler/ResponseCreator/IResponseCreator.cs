using AtomCore.CCC.ExceptionHandling.RestAPIHandler.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.CCC.ExceptionHandling.RestAPIHandler.ResponseCreator;

public interface IResponseCreator
{
    string GetContentType();

    Task HandleException(BusinessException exception);
    Task HandleException(Exception exception);
}


