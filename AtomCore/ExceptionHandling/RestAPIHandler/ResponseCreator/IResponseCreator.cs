using AtomCore.ExceptionHandling.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.ExceptionHandling.RestAPIHandler.ResponseCreator;

public interface IResponseCreator
{
    string GetContentType();

    Task HandleException(BusinessException exception);
    Task HandleException(ValidationException exception);
    Task HandleException(AuthenticationException exception);
    Task HandleException(AuthorizationException exception);
    Task HandleException(InfrastructureException exception);
    Task HandleException(Exception exception);
}
