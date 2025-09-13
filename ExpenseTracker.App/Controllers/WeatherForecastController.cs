using ExpenseTracker.Contracts.Logging.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILoggerManager<WeatherForecastController> _logger;

        public WeatherForecastController(ILoggerManager<WeatherForecastController> logger, Contracts.Logging.ILoggerManager logger2)
        {
            _logger = logger;

            _logger.LogDebug("CTOR DBG");
            _logger.LogInfo("CTOR INFO");
            _logger.LogError("CTOR ERR");

            logger2.LogDebug("CTOR DBG");
            logger2.LogInfo("CTOR INFO");
            logger2.LogError("CTOR ERR");
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
