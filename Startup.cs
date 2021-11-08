using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using Protomolecule.Extensions;

namespace Protomolecule
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.SetupSwagger();
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application.ConfigureSwagger(environment)
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseMetricServer()
                .UseHttpMetrics()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
