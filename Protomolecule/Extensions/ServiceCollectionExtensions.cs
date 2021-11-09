using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Protomolecule.Services;
using Protomolecule.Utility;

namespace Protomolecule.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SetupSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = configuration["Swagger:ApiName"], Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "Open Id" }
                            },
                            AuthorizationUrl = new Uri(configuration["Authentication:Auth0:Domain"] + "/authorize?audience=" + configuration["Authentication:Auth0:Audience"])
                        }
                    }
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }

        public static IServiceCollection SetupAuth0(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = configuration["Authentication:Auth0:Domain"];
                options.Audience = configuration["Authentication:Auth0:Audience"];
            });

            return services;
        }

        public static IServiceCollection SetupControllersToAuthorizeByDefault(this IServiceCollection services)
        {
            services.AddControllers(o =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                o.Filters.Add(new AuthorizeFilter(policy));
            });

            return services;
        }

        public static IServiceCollection SetupDependencies(this IServiceCollection services)
        {
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();

            return services;
        }
    }
}
