using System.Text.Json.Serialization;

namespace WeatherForecast.Api.Models;

public class OpenMeteoResponse
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    [JsonPropertyName("current_weather")]
    public CurrentWeather? CurrentWeather { get; set; }

    [JsonPropertyName("hourly_units")]
    public HourlyUnits? HourlyUnits { get; set; }

    [JsonPropertyName("hourly")]
    public HourlyData? Hourly { get; set; }

    [JsonPropertyName("daily_units")]
    public DailyUnits? DailyUnits { get; set; }

    [JsonPropertyName("daily")]
    public DailyData? Daily { get; set; }
}

public class CurrentWeather
{
    [JsonPropertyName("time")]
    public string? Time { get; set; }

    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("windspeed")]
    public double WindSpeed { get; set; }

    [JsonPropertyName("winddirection")]
    public double WindDirection { get; set; }

    [JsonPropertyName("weathercode")]
    public int WeatherCode { get; set; }

    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }
}

public class HourlyUnits
{
    [JsonPropertyName("time")]
    public string? Time { get; set; }

    [JsonPropertyName("temperature_2m")]
    public string? Temperature { get; set; }

    [JsonPropertyName("precipitation")]
    public string? Precipitation { get; set; }

    [JsonPropertyName("windspeed_10m")]
    public string? WindSpeed { get; set; }

    [JsonPropertyName("weathercode")]
    public string? WeatherCode { get; set; }
}

public class HourlyData
{
    [JsonPropertyName("time")]
    public List<string>? Time { get; set; }

    [JsonPropertyName("temperature_2m")]
    public List<double>? Temperature { get; set; }

    [JsonPropertyName("precipitation")]
    public List<double>? Precipitation { get; set; }

    [JsonPropertyName("windspeed_10m")]
    public List<double>? WindSpeed { get; set; }

    [JsonPropertyName("weathercode")]
    public List<int>? WeatherCode { get; set; }
}

public class DailyUnits
{
    [JsonPropertyName("time")]
    public string? Time { get; set; }

    [JsonPropertyName("temperature_2m_max")]
    public string? TemperatureMax { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public string? TemperatureMin { get; set; }

    [JsonPropertyName("precipitation_sum")]
    public string? PrecipitationSum { get; set; }

    [JsonPropertyName("weathercode")]
    public string? WeatherCode { get; set; }
}

public class DailyData
{
    [JsonPropertyName("time")]
    public List<string>? Time { get; set; }

    [JsonPropertyName("temperature_2m_max")]
    public List<double>? TemperatureMax { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public List<double>? TemperatureMin { get; set; }

    [JsonPropertyName("precipitation_sum")]
    public List<double>? PrecipitationSum { get; set; }

    [JsonPropertyName("weathercode")]
    public List<int>? WeatherCode { get; set; }
}
