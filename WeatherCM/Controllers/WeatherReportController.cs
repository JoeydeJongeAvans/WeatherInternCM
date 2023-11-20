using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherCM.Domains;
using System.Configuration;

namespace WeatherCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public WeatherController(IConfiguration configuration)
        {
            _client = new HttpClient();
            _configuration = configuration;

            _client.DefaultRequestHeaders.Add("X-RapidAPI-Key", _configuration["X-RapidAPI-Key"]);
            _client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");
        }


        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentWeather([FromBody] WeatherRequestModel requestModel)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://weatherapi-com.p.rapidapi.com/current.json?q={requestModel.City}")
                };

                using (var response = await _client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    return Ok(body);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        [HttpGet("forecast")]
        public async Task<IActionResult> GetWeatherForecast([FromBody] WeatherRequestModel requestModel)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://weatherapi-com.p.rapidapi.com/forecast.json?q={requestModel.City}&days=3")
                };

                using (var response = await _client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    return Ok(body);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

     

     


    }


}
