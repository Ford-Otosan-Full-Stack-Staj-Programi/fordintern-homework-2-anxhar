using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using OdevApi.Web;

namespace OdevApi.Web.Extension;

public static class StartUpSwaggerExtension
{
    public static void AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Odev Api Management", Version = "v1.0" });
            c.OperationFilter<ExtensionSwaggerFileOperationFilter>();

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Odev Api Management",
                Description = "JWT bearer token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
        });
    }

}
