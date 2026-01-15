using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtomCore.JWT.Options;
using AtomCore.JWT.Pipeline;
using MediatR;

namespace AtomCore.JWT;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddJwtHelper(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<JwtTokenHelper>();

        return services;
    }

    public static IServiceCollection AddTokenCheckPipeline(this IServiceCollection services, string accessTokenConfigSectionName)
    {
        IConfiguration config=services.BuildServiceProvider().GetService<IConfiguration>()!;
        
        services.AddOptions<TokenValidationOptions>()
            .Bind(config.GetSection(accessTokenConfigSectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TokenCheckPipeline<,>));
        
        return services;
    }
}