using WeatherForecast.Api.Models;

namespace WeatherForecast.Api.Services;

public interface IWeatherService
{
    Task<WeatherForecastResponse> GetForecastAsync(double latitude, double longitude, int days = 7, CancellationToken cancellationToken = default);
    Task<CurrentWeatherInfo> GetCurrentWeatherAsync(double latitude, double longitude, CancellationToken cancellationToken = default);
}
