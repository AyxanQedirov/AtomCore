using System.IdentityModel.Tokens.Jwt;

namespace AtomCore.JWT;

public static class JwtSecurityTokenExtensions
{
    public static string? GetClaim(this JwtSecurityToken token, string name)
    {
        return token.Claims.FirstOrDefault(c => c.Type == name)?.Value;
    }
}