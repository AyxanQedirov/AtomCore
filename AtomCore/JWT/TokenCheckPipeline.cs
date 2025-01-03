using MediatR;
using System.Security.Authentication;

namespace AtomCore.JWT;

public class TokenCheckPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly JwtTokenHelper _jwtTokenHelper;

    public TokenCheckPipeline(JwtTokenHelper jwtTokenHelper)
    {
        _jwtTokenHelper = jwtTokenHelper;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        bool ignored = request.GetType().GetCustomAttributes(true).Where(a => a.GetType() == typeof(IgnoreTokenCheckAttribute)).Any();

        if (!ignored)
        {
            string? token = _jwtTokenHelper.GetCurrentToken();
            bool isValid = _jwtTokenHelper.ValidateToken(token);

            if (!isValid)
                throw new AuthenticationException("Your JWT token is not valid");
        }

        var response = next();

        return response;
    }
}
