using Application.Configurations;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoInfrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoInfrastructure
{
    public static class ConfigureServices
    {
        public static void AddMongoInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureMongoContext(services);
            ConfigureRepositoryManager(services);
            ConfigureServiceManager(services);
            AddMongoContextConfiguration(services, configuration);
        }
        public static void AddMongoContextConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbConfiguration>(configuration.GetSection("CustomersAccountsDatabase"));
        }
        public static void ConfigureMongoContext(IServiceCollection services)
        {
            services.AddScoped<IMongoContext, MongoContext>();
        }
        public static void ConfigureRepositoryManager(IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, MongoRepositoryManager>();
        }
        public static void ConfigureServiceManager(IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }
    }
}
