using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Api.Models;
using WeatherForecast.Api.Services;

namespace WeatherForecast.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class WeatherController(IWeatherService weatherService) : ControllerBase
{
    /// <summary>
    /// Gets the weather forecast for a given location.
    /// </summary>
    /// <param name="latitude">Latitude of the location (e.g., 41.9028 for Rome).</param>
    /// <param name="longitude">Longitude of the location (e.g., 12.4964 for Rome).</param>
    /// <param name="days">Number of forecast days (1–16). Defaults to 7.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Weather forecast including current conditions, daily and hourly data.</returns>
    [HttpGet("forecast")]
    [ProducesResponseType(typeof(WeatherForecastResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> GetForecast(
        [FromQuery] double latitude,
        [FromQuery] double longitude,
        [FromQuery] int days = 7,
        CancellationToken cancellationToken = default)
    {
        if (latitude < -90 || latitude > 90)
            return BadRequest("Latitude must be between -90 and 90.");

        if (longitude < -180 || longitude > 180)
            return BadRequest("Longitude must be between -180 and 180.");

        if (days < 1 || days > 16)
            return BadRequest("Days must be between 1 and 16.");

        var forecast = await weatherService.GetForecastAsync(latitude, longitude, days, cancellationToken);
        return Ok(forecast);
    }

    /// <summary>
    /// Gets the current weather for a given location.
    /// </summary>
    /// <param name="latitude">Latitude of the location (e.g., 41.9028 for Rome).</param>
    /// <param name="longitude">Longitude of the location (e.g., 12.4964 for Rome).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Current weather conditions.</returns>
    [HttpGet("current")]
    [ProducesResponseType(typeof(CurrentWeatherInfo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> GetCurrentWeather(
        [FromQuery] double latitude,
        [FromQuery] double longitude,
        CancellationToken cancellationToken = default)
    {
        if (latitude < -90 || latitude > 90)
            return BadRequest("Latitude must be between -90 and 90.");

        if (longitude < -180 || longitude > 180)
            return BadRequest("Longitude must be between -180 and 180.");

        var current = await weatherService.GetCurrentWeatherAsync(latitude, longitude, cancellationToken);
        return Ok(current);
    }
}
