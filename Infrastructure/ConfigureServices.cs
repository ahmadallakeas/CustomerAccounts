﻿using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static void AddInfrastrcutureServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDbContext(services, configuration);
            ConfigureRepositoryManager(services);
        }
        public static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("default")));
        }
        public static void ConfigureRepositoryManager(IServiceCollection services)
        {
       //     services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
    }
}
