using System.Net.Http.Json;
using WeatherForecast.Api.Models;

namespace WeatherForecast.Api.Services;

public class WeatherService(HttpClient httpClient) : IWeatherService
{
    private const string HourlyVariables = "temperature_2m,precipitation,windspeed_10m,weathercode";
    private const string DailyVariables = "temperature_2m_max,temperature_2m_min,precipitation_sum,weathercode";

    public async Task<WeatherForecastResponse> GetForecastAsync(
        double latitude,
        double longitude,
        int days = 7,
        CancellationToken cancellationToken = default)
    {
        var url = BuildForecastUrl(latitude, longitude, days);
        var response = await httpClient.GetFromJsonAsync<OpenMeteoResponse>(url, cancellationToken)
            ?? throw new InvalidOperationException("No response received from Open-Meteo API.");

        return MapToForecastResponse(response);
    }

    public async Task<CurrentWeatherInfo> GetCurrentWeatherAsync(
        double latitude,
        double longitude,
        CancellationToken cancellationToken = default)
    {
        var url = $"forecast?latitude={latitude}&longitude={longitude}&current_weather=true&timezone=auto";
        var response = await httpClient.GetFromJsonAsync<OpenMeteoResponse>(url, cancellationToken)
            ?? throw new InvalidOperationException("No response received from Open-Meteo API.");

        if (response.CurrentWeather is null)
            throw new InvalidOperationException("Current weather data is unavailable.");

        return MapToCurrentWeatherInfo(response.CurrentWeather);
    }

    private static string BuildForecastUrl(double latitude, double longitude, int days)
    {
        return $"forecast?latitude={latitude}&longitude={longitude}" +
               $"&hourly={HourlyVariables}" +
               $"&daily={DailyVariables}" +
               $"&current_weather=true" +
               $"&timezone=auto" +
               $"&forecast_days={days}";
    }

    private static WeatherForecastResponse MapToForecastResponse(OpenMeteoResponse response)
    {
        var result = new WeatherForecastResponse
        {
            Latitude = response.Latitude,
            Longitude = response.Longitude,
            Timezone = response.Timezone,
            Elevation = response.Elevation
        };

        if (response.CurrentWeather is not null)
            result.Current = MapToCurrentWeatherInfo(response.CurrentWeather);

        if (response.Daily is not null)
        {
            var count = response.Daily.Time?.Count ?? 0;
            for (int i = 0; i < count; i++)
            {
                var weatherCode = response.Daily.WeatherCode?[i] ?? 0;
                result.DailyForecasts.Add(new DailyForecast
                {
                    Date = response.Daily.Time?[i],
                    MaxTemperatureCelsius = response.Daily.TemperatureMax?[i] ?? 0,
                    MinTemperatureCelsius = response.Daily.TemperatureMin?[i] ?? 0,
                    PrecipitationMm = response.Daily.PrecipitationSum?[i] ?? 0,
                    WeatherCode = weatherCode,
                    WeatherDescription = GetWeatherDescription(weatherCode)
                });
            }
        }

        if (response.Hourly is not null)
        {
            var count = response.Hourly.Time?.Count ?? 0;
            for (int i = 0; i < count; i++)
            {
                var weatherCode = response.Hourly.WeatherCode?[i] ?? 0;
                result.HourlyForecasts.Add(new HourlyForecast
                {
                    Time = response.Hourly.Time?[i],
                    TemperatureCelsius = response.Hourly.Temperature?[i] ?? 0,
                    PrecipitationMm = response.Hourly.Precipitation?[i] ?? 0,
                    WindSpeedKmh = response.Hourly.WindSpeed?[i] ?? 0,
                    WeatherCode = weatherCode,
                    WeatherDescription = GetWeatherDescription(weatherCode)
                });
            }
        }

        return result;
    }

    private static CurrentWeatherInfo MapToCurrentWeatherInfo(CurrentWeather current)
    {
        return new CurrentWeatherInfo
        {
            Time = current.Time,
            TemperatureCelsius = current.Temperature,
            WindSpeedKmh = current.WindSpeed,
            WindDirectionDegrees = current.WindDirection,
            WeatherCode = current.WeatherCode,
            WeatherDescription = GetWeatherDescription(current.WeatherCode),
            IsDay = current.IsDay == 1
        };
    }

    // WMO Weather interpretation codes: https://open-meteo.com/en/docs
    private static string GetWeatherDescription(int weatherCode) => weatherCode switch
    {
        0 => "Clear sky",
        1 => "Mainly clear",
        2 => "Partly cloudy",
        3 => "Overcast",
        45 => "Fog",
        48 => "Depositing rime fog",
        51 => "Light drizzle",
        53 => "Moderate drizzle",
        55 => "Dense drizzle",
        61 => "Slight rain",
        63 => "Moderate rain",
        65 => "Heavy rain",
        71 => "Slight snow fall",
        73 => "Moderate snow fall",
        75 => "Heavy snow fall",
        77 => "Snow grains",
        80 => "Slight rain showers",
        81 => "Moderate rain showers",
        82 => "Violent rain showers",
        85 => "Slight snow showers",
        86 => "Heavy snow showers",
        95 => "Thunderstorm",
        96 => "Thunderstorm with slight hail",
        99 => "Thunderstorm with heavy hail",
        _ => "Unknown"
    };
}
