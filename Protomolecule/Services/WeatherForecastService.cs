using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Protomolecule.Controllers;
using Protomolecule.Models;

namespace Protomolecule.Services
{
    public interface IWeatherForecastService
    {
        public IReadOnlyCollection<WeatherForecast> GetForecast();
    }

    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<WeatherForecastController> _logger;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastService(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IReadOnlyCollection<WeatherForecast> GetForecast()
        {
            var random = new Random();

            var forecasts = Enumerable.Range(1, 5)
                .Select(index => new WeatherForecast
                {
                    Date = DateTime.UtcNow.AddDays(index),
                    TemperatureC = random.Next(-20, 55),
                    Summary = Summaries[random.Next(Summaries.Length)]
                })
                .ToArray();

            _logger.LogDebug($"Generated {forecasts.Length} forecasts");

            return forecasts;
        }
    }
}
