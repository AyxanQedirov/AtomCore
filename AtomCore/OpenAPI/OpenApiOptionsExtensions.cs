using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace AtomCore.OpenAPI;

public static class OpenApiOptionsExtensions
{
    public static OpenApiOptions AddJwtAuthentication(this OpenApiOptions options)
    {
        options.AddDocumentTransformer((document, _, _) =>
        {
            document.Components ??= new OpenApiComponents();

            document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "Enter your JWT token (without �Bearer � prefix)"
            };

            document.SecurityRequirements.Add(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id   = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            return  Task.CompletedTask;
        });

        return options;
        
    }
}