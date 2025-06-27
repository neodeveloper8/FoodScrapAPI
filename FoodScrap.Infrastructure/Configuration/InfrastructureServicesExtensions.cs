using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace FoodScrap.Infrastructure.Configuration
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            // Database Connection
            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var connectionString = configuration.GetConnectionString("CadenaSQL");
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}
