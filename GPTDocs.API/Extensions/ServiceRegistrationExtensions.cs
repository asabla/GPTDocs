using Microsoft.OpenApi.Models;

namespace GPTDocs.API.Extensions;

internal static class ServiceRegistrationExtensions
{
    public static WebApplicationBuilder RegisterSwagger(
        this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsProduction())
        {
            var openAPIUrls = builder.Configuration["OPENAPI_URLS"] ?? string.Empty;
            builder.Services.AddSwaggerGen(option =>
            {
                // These urls are strictly used for generated documentation
                // and does not affect the actual API
                foreach (var url in openAPIUrls.Split(';'))
                {
                    option.AddServer(new OpenApiServer
                    {
                        Url = url,
                        Description = "Production"
                    });
                }
            });
        }
        else
        {
            builder.Services.AddSwaggerGen();
        }

        return builder;
    }
}
