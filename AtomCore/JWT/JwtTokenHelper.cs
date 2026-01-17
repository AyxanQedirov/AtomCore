using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AtomCore.ExceptionHandling.Exceptions;
using AtomCore.JWT.Options;
using Microsoft.Extensions.Options;

namespace AtomCore.JWT;

public class JwtTokenHelper(
    IOptions<TokenValidationOptions> tokenValidationOpt,
    IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor)
{
    private readonly IConfiguration _configuration = configuration;

    public string CreateToken(params Claim[] claims)
    {
        return CreateToken(tokenValidationOpt.Value, claims);
    }

    public string CreateToken(ITokenOption tokenOption, params Claim[] claims)
    {
        return CreateToken(
            tokenOption.SecretKey,
            tokenOption.ExpireTime,
            tokenOption.Audience,
            tokenOption.Issuer, claims);
    }

    private string CreateToken(string secretKey, TimeSpan expireTime, string audience, string issuer,
        params Claim[] claims)
    {
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(secretKey));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);
        DateTime expirationDate = DateTime.Now.Add(expireTime);


        JwtSecurityToken jwtSecurityToken = new(
            claims: claims,
            audience: audience,
            issuer: issuer,
            expires: expirationDate,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials
        );

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        return jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
    }

    public bool ValidateToken(ITokenOption tokenOption, string rawToken, bool checkExpiration = true)
    {
        return ValidateToken(rawToken, tokenOption.SecretKey, tokenOption.Audience, tokenOption.Issuer,
            checkExpiration);
    }

    private bool ValidateToken(string rawToken, string secretKey, string audience, string issuer,
        bool checkExpiration = true)
    {
        if (rawToken is null)
            throw new AuthenticationException("Token which you give is null");

        string token = NormalizeRawToken(rawToken);

        JwtSecurityTokenHandler tokenHandler = new();
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(secretKey));

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = checkExpiration,
                ValidateLifetime = checkExpiration,
                IssuerSigningKey = securityKey,
                ValidIssuer = issuer,
                ValidAudience = audience
            }, out _);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public string NormalizeRawToken(string rawToken)
    {
        string targetWord = "Bearer";
        int targetWordStartIndex = rawToken.IndexOf(targetWord);

        if (targetWordStartIndex < 0)
            return rawToken;

        string normalizedToken = rawToken.Remove(targetWordStartIndex, targetWord.Length).Trim();
        return normalizedToken;
    }

    public JwtSecurityToken DecodeToken(string token)
    {
        string normalizedToken = NormalizeRawToken(token);
        JwtSecurityTokenHandler tokenHandler = new();
        JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(normalizedToken);

        return jwtToken;
    }

    public string? GetCurrentToken()
    {
        string? rawToken = httpContextAccessor.HttpContext?.Request.Headers.Authorization;

        return rawToken;
    }

    public JwtSecurityToken GetCurrentDecodedToken()
    {
        string? rawToken = GetCurrentToken();

        if (rawToken is null) throw new ArgumentNullException("Token was not settd");

        return DecodeToken(rawToken);
    }

    public string? GetClaim(string key)
    {
        return GetCurrentDecodedToken().Claims.FirstOrDefault(claim => claim.Type == key)?.Value;
    }
}