namespace WeatherForecast.Api.Models;

public class WeatherForecastResponse
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Timezone { get; set; }
    public double Elevation { get; set; }
    public CurrentWeatherInfo? Current { get; set; }
    public List<DailyForecast> DailyForecasts { get; set; } = [];
    public List<HourlyForecast> HourlyForecasts { get; set; } = [];
}

public class CurrentWeatherInfo
{
    public string? Time { get; set; }
    public double TemperatureCelsius { get; set; }
    public double WindSpeedKmh { get; set; }
    public double WindDirectionDegrees { get; set; }
    public int WeatherCode { get; set; }
    public string? WeatherDescription { get; set; }
    public bool IsDay { get; set; }
}

public class DailyForecast
{
    public string? Date { get; set; }
    public double MaxTemperatureCelsius { get; set; }
    public double MinTemperatureCelsius { get; set; }
    public double PrecipitationMm { get; set; }
    public int WeatherCode { get; set; }
    public string? WeatherDescription { get; set; }
}

public class HourlyForecast
{
    public string? Time { get; set; }
    public double TemperatureCelsius { get; set; }
    public double PrecipitationMm { get; set; }
    public double WindSpeedKmh { get; set; }
    public int WeatherCode { get; set; }
    public string? WeatherDescription { get; set; }
}
