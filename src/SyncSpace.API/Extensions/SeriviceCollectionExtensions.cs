using Microsoft.OpenApi.Models;
using SyncSpace.API.Middlewares;

namespace SyncSpace.API.Extensions;

public static class SeriviceCollectionExtensions
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddScoped<ErrorHandlingMiddleware>();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme , Id="BearerAuth"}
                    },
                    []
                }
            });
        });
    }
}
