using Application.Behaviors;
using Application.Configurations;
using Application.ErrorModels;
using Application.Exceptions;
using Application.formatters;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoRepository.Repository;
using Serilog;
using Serilog.Events;
using Services;
using SqlRepository.Repository;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace UsersService.API
{
    public static class ConfigureServices
    {
        public static void AddCustomersServiceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureCors();
            ConfigureSerilog();
            ConfigureAutoMapper(services);
            AddJwtConfiguration(services, configuration);
            ConfigureJWT(services, configuration);
            ConfigureMediatR(services);
            ConfigureFluentValidation(services);
            ConfigureValidationBehavior(services);
            ConfigureSwagger(services);
            ConfigureServiceManager(services);
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
                 path: "C:\\Logs\\Customers-.log",
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
                               ValidationAppException => StatusCodes.Status422UnprocessableEntity,
                               _ => StatusCodes.Status500InternalServerError
                           };
                           Log.Error($"Something went wrong: {contextFeature.Error}");
                           if (contextFeature.Error is ValidationAppException exception)
                           {

                               await context.Response.WriteAsync(JsonSerializer.Serialize(new { exception.Errors }));

                           }
                           else
                           {
                               await context.Response.WriteAsync(new ErrorDetails()
                               {
                                   StatusCode = context.Response.StatusCode,
                                   Message = contextFeature.Error.Message,
                               }.ToString());
                           }
                       }
                   }
                    );
            });
        }
        public static void ConfigureServiceManager(IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
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
            services.Configure<JwtConfiguration>(configuration.GetSection("CustomersSettings"));
        }
        public static void ConfigureMediatR(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(assembly);
        }
        public static void ConfigureFluentValidation(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(assembly);
        }
        public static void ConfigureValidationBehavior(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
        public static void ConfigureSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("default")));

            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
        public static void ConfigureMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbConfiguration>(configuration.GetSection("CustomersDatabase"));
            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IRepositoryManager, MongoRepositoryManager>();
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