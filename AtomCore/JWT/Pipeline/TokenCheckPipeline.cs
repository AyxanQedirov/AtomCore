using MediatR;
using System.Security.Authentication;
using Microsoft.Extensions.Options;
using AuthenticationException = AtomCore.ExceptionHandling.Exceptions.AuthenticationException;

namespace AtomCore.JWT;

public class TokenCheckPipeline<TRequest, TResponse>(JwtTokenHelper jwtTokenHelper, IOptions<TokenValidationOptions> options) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly TokenValidationOptions option = options.Value;
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        bool ignored = request.GetType().GetCustomAttributes(true).Where(a => a.GetType() == typeof(IgnoreTokenCheckAttribute)).Any();

        if (!ignored)
        {
            string? token = jwtTokenHelper.GetCurrentToken();
            bool isValid = jwtTokenHelper.ValidateToken(option,token);

            if (!isValid)
                throw new AuthenticationException("Your JWT token is not valid");
        }

        var response = next();

        return response;
    }
}
