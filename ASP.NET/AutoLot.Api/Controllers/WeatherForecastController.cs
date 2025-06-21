namespace AutoLot.Api.Controllers;

[ApiVersionNeutral]
[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Retrieves a collection of weather forecasts for the next five days.
    /// </summary>
    /// <remarks>The temperature values are randomly generated within the range of -20 to 55 degrees Celsius,
    /// and the summary is selected randomly from a predefined set of descriptions.</remarks>
    /// <returns>An <see cref="IEnumerable{T}"/> containing five <see cref="WeatherForecast"/> objects, each representing a
    /// forecast for a specific day. The forecasts include the date, temperature in Celsius, and a summary description.</returns>
    [HttpGet]
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
