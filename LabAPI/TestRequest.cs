using System.Security.Claims;
using AtomCore.JWT;
using AtomCore.JWT.Pipeline;
using FluentValidation;
using MediatR;

namespace LabAPI;

[IgnoreTokenCheck]
public class TokenTestRequest : IRequest<TokenTestRequestResponse>
{
    public string Username { get; set; }
}

public class TokenTestRequestHandler(JwtTokenHelper tokenHelper)
    : IRequestHandler<TokenTestRequest, TokenTestRequestResponse>
{
    public Task<TokenTestRequestResponse> Handle(TokenTestRequest request, CancellationToken cancellationToken)
    {
        var response = new TokenTestRequestResponse();

        response.AccessToken = tokenHelper.CreateToken(new Claim("username", request.Username));

        return Task.FromResult(response);
    }
}

public class TokenTestRequestResponse
{
    public string AccessToken { get; set; }
}

public class TokenTestRequestValidator : AbstractValidator<TokenTestRequest>
{
    public TokenTestRequestValidator()
    {
    }
}