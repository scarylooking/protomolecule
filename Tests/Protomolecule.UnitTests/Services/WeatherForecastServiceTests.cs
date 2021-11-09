using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Protomolecule.Controllers;
using Protomolecule.Services;
using Xunit;

namespace Protomolecule.UnitTests.Services
{
    public class WeatherForecastServiceTests
    {
        private readonly IMock<IConfiguration> _configuration;
        private readonly IMock<ILogger<WeatherForecastController>> _logger;

        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastServiceTests()
        {
            _configuration = new Mock<IConfiguration>();
            _logger = new Mock<ILogger<WeatherForecastController>>();

            _weatherForecastService = new WeatherForecastService(_logger.Object, _configuration.Object);
        }

        [Fact]
        public void GetForecast_Returns_FiveItems()
        {
            var result = _weatherForecastService.GetForecast();

            result.Should().HaveCount(5, "service should return 5 weather forecasts");
        }
    }
}