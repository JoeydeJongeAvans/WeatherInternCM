// Program.cs
using Microsoft.Extensions.DependencyInjection;
using WeatherCM.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Gebruik hier ISendSMSService in plaats van SendSMSService
builder.Services.AddSingleton<ISendSMSService, SendSMSService>();
builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddSingleton<IWeatherService, WeatherService>();

var app = builder.Build();

// Start the weather update task when the application starts
var weatherService = app.Services.GetRequiredService<WeatherService>();
weatherService.StartGetAstromy();
weatherService.StartGetCurrentWeather();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Voeg extra logging toe
app.Run();
