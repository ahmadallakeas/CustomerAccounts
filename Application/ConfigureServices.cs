using Application.Configurations;
using Application.ErrorModels;
using Application.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
        public static void AddApplicationServices(this IServiceCollection services)
        {

            ConfigureSerilog();
            ConfigureAutoMapper(services);
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
                               BadRequestException=>StatusCodes.Status400BadRequest,
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
    }
}