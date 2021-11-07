using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Protomolecule.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SetupSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Protomolecule", Version = "v1" });
            });

            return services;
        }
    }
}
