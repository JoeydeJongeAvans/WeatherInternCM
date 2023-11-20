using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherCM.Domains;

namespace WeatherCM.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _client;
        private readonly ILogger<WeatherService> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISendSMSService _sendSMSService;
        private Timer _weatherUpdateTimer;

        public WeatherService(HttpClient client, ILogger<WeatherService> logger, IConfiguration configuration, ISendSMSService sendSMSService)
        {

            _logger = logger;
            _client = client;
            _configuration = configuration;
            _sendSMSService = sendSMSService;

            _client.DefaultRequestHeaders.Add("X-RapidAPI-Key", _configuration["X-RapidAPI-Key"]);
            _client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");
        }

         public void StartGetCurrentWeather()
         {
             // Set up a timer to run the task every 10 seconds
             _weatherUpdateTimer = new Timer(GetCurrentWeather, null, TimeSpan.Zero, TimeSpan.FromDays(1));
         }

        public void StartGetAstromy()
        {
            _weatherUpdateTimer = new Timer(GetAstronomyToday, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        }



        /// <summary>
        /// Get the Current Weather from the API and send the information to SendSMS method
        /// </summary>
        /// <param name="state"></param>
        private void GetCurrentWeather(object state)
        {
            try
            {
                // Create a request model (replace this with actual data)
                var requestModel = new WeatherRequestModel { City = "Goes" };

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://weatherapi-com.p.rapidapi.com/current.json?q={requestModel.City}")
                };

                using (var response = _client.SendAsync(request).Result)
                {
                    response.EnsureSuccessStatusCode();
                    var body = response.Content.ReadAsStringAsync().Result;
                    var formattedResponse = FormatCurrentWeatherResponse(body);

                    // Log the output
                    _logger.LogInformation($"WeatherJoey: {formattedResponse}");
                   /* _sendSMSService.SendCurrentWeatherSMS(formattedResponse);*/
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during the task
                _logger.LogError($"Error getting information weather: {ex.Message}");
            }
        }


        /// <summary>
        /// Get the Current Weather from the API and send the information to SendSMS method
        /// </summary>
        /// <param name="state"></param>
        private void GetAstronomyToday(object state)
        {
            try
            {
                // Create a request model (replace this with actual data)
                var requestModel = new WeatherRequestModel { City = "Goes" };

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://weatherapi-com.p.rapidapi.com/astronomy.json?q={requestModel.City}")
                };

                using (var response = _client.SendAsync(request).Result)
                {
                    response.EnsureSuccessStatusCode();
                    var body = response.Content.ReadAsStringAsync().Result;
                    var formattedResponse = FormatAstronomy(body);

                    // Log the output
                    _logger.LogInformation($"WeatherJoey: {formattedResponse}");
                    /*_sendSMSService.SendAstronomySMS(formattedResponse);*/
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during the task
                _logger.LogError($"Error getting information weather: {ex.Message}");
            }
        }


        private dynamic FormatCurrentWeatherResponse(string json)
        {
            var weatherData = JsonConvert.DeserializeObject<WeatherResponse>(json);
            return $"Hello, the weather in {weatherData.Location.Name} is at the moment {weatherData.Current.TempC}°C.";
        }

        private dynamic FormatAstronomy(string json)
        {
            var astronomyData = JsonConvert.DeserializeObject<WeatherResponse>(json);
            return $"Hello, the Sunrise: {astronomyData.Astronomy.Astro.Sunrise}, Sunset: {astronomyData.Astronomy.Astro.Sunset} Moonphase: {astronomyData.Astronomy.Astro.MoonPhase}";
        }


    }
}
