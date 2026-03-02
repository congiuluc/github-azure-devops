using Microsoft.OpenApi;
using WeatherForecast.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddProblemDetails();

builder.Services.AddHttpClient<IWeatherService, WeatherService>(client =>
{
    client.BaseAddress = new Uri("https://api.open-meteo.com/v1/");
});

builder.Services.AddHealthChecks();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Weather Forecast API",
        Version = "v1",
        Description = "A .NET 10 REST API that provides weather forecasts using the Open-Meteo API (https://open-meteo.com/).",
        Contact = new OpenApiContact
        {
            Name = "Open-Meteo",
            Url = new Uri("https://open-meteo.com/")
        }
    });

    // Include XML comments for Swagger documentation
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather Forecast API v1");
    options.RoutePrefix = string.Empty; // Serve Swagger UI at root
});

app.UseHttpsRedirection();
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
