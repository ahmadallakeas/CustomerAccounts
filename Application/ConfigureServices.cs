using Application.Configurations;
using Application.ErrorModels;
using Application.Exceptions;
using Application.formatters;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ConfigureServices
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureCors(services);
            ConfigureSerilog();
            ConfigureAutoMapper(services);
            AddJwtConfiguration(services, configuration);
            ConfigureJWT(services, configuration);
            ConfigureMediatR(services);
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowAll", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );
            });
        }
        public static void ConfigureSerilog()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                 path: "C:\\Logs\\CustomerAccountLog-.log",
                 outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                 rollingInterval: RollingInterval.Day,
                 restrictedToMinimumLevel: LogEventLevel.Information)
                   .CreateLogger();

        }
        public static void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(
                   async context =>
                   {
                       context.Response.ContentType = "application/json";
                       var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                       if (contextFeature != null)
                       {
                           context.Response.StatusCode = contextFeature.Error
                           switch
                           {
                               NotFoundException => StatusCodes.Status404NotFound,
                               BadRequestException => StatusCodes.Status400BadRequest,
                               _ => StatusCodes.Status500InternalServerError
                           };
                           Log.Error($"Something went wrong: {contextFeature.Error}");
                           await context.Response.WriteAsync(new ErrorDetails()
                           {
                               StatusCode = context.Response.StatusCode,
                               Message = contextFeature.Error.Message,
                           }.ToString());
                       }
                   }
                    );
            });
        }
        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder)
          => builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));

        public static void ConfigureJWT(IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfiguration = new JwtConfiguration();
            configuration.Bind(jwtConfiguration.Section, jwtConfiguration);
            var secretKey = Environment.GetEnvironmentVariable("CAKEY");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtConfiguration.ValidIssuer,
                    ValidAudience = jwtConfiguration.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))

                };
            });
        }
        public static void AddJwtConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection("CustomersAccountsJwtSettings"));
        }
        public static void ConfigureMediatR(IServiceCollection services)
        {
            services.AddMediatR(typeof(AssemblyReference).Assembly);
        }
    }
}