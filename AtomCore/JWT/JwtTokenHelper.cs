﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AtomCore.JWT;

public class JwtTokenHelper
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtTokenHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public string CreateToken(params Claim[] claims)
    {
        string key = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("Key was not settd. appsettings.json > Jwt > Key");
        string expireMinute = _configuration["Jwt:ExpiredDateAsMinute"] ?? throw new ArgumentNullException("ExpiredDateAsMinute was not settd. appsettings.json > Jwt > ExpiredDateAsMinute");
        string audience = _configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Audience was not settd. appsettings.json > Jwt > Audience");
        string issuer = _configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Issuer was not settd. appsettings.json > Jwt > Issuer");


        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);
        DateTime expirationDate = DateTime.Now.AddMinutes(int.Parse(expireMinute));


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
    public bool ValidateToken(string rawToken)
    {
        if (rawToken is null)
            throw new ArgumentNullException("Token which you give is null");

        string key = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("Key was not settd. appsettings.json > Jwt > Key");
        string audience = _configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Audience was not settd. appsettings.json > Jwt > Audience");
        string issuer = _configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Issuer was not settd. appsettings.json > Jwt > Issuer");



        string token = NormalizeRawToken(rawToken);

        JwtSecurityTokenHandler tokenHandler = new();
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
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
        string? rawToken = _httpContextAccessor.HttpContext?.Request.Headers.Authorization;

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