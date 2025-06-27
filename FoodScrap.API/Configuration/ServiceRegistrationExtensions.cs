using Microsoft.OpenApi.Models;
using FoodScrap.Infrastructure.Configuration;

namespace FoodScrap.API.Configuration
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            // Registro de servicios de Infrastructura
            services.AddInfrastructureServices(configuration);

            // Habilitar Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "FoodScrap.API", Version = "v1" });
            });

            return services;
        }
    }
}
