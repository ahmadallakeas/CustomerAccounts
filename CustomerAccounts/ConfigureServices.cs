using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Presentation
{
    public static class ConfigureServices
    {
        public static void AddPresenationServices(this IServiceCollection services)
        {
            ConfigureSwagger(services);
        }

        public static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "CustomersAccounts Api",
                        Version = "1.0",
                        Description = "An API to manage accounts of customers",
                        Contact = new OpenApiContact
                        {
                            Name = "Ahmad Al Lakeas",
                            Email = "ahmadallakeas@gmail.com"
                        }
                    });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Place to add JWT with Bearer",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference
                            { Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"}, Name = "Bearer", },
                        new List<string>()
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }
    }
}
