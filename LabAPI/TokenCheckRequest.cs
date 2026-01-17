using AtomCore.JWT;
using AtomCore.JWT.Options;
using AtomCore.JWT.Pipeline;
using MediatR;
using Microsoft.Extensions.Options;

namespace LabAPI;

[IgnoreTokenCheck]
public class TokenCheckRequest : IRequest<TokenCheckResponse>
{
    public string Token { get; set; }
}

public class TokenCheckHandler(JwtTokenHelper tokenHelper, IOptions<TokenValidationOptions> tokenOpt)
    : IRequestHandler<TokenCheckRequest, TokenCheckResponse>
{
    public async Task<TokenCheckResponse> Handle(TokenCheckRequest request, CancellationToken cancellationToken)
    {
        var isValid = tokenHelper.ValidateToken(tokenOpt.Value, request.Token);

        return new()
        {
            IsValid = isValid,
        };
    }
}

public class TokenCheckResponse
{
    public bool IsValid { get; set; }
}