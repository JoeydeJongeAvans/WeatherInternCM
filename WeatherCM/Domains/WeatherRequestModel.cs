namespace WeatherCM.Domains
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the model for a weather request, specifying the desired city.
    /// </summary>
    public class WeatherRequestModel
    {

        /// Gets or sets the required city for the weather request.
        /// </summary>
        public string City { get; set; }
    }

    /// <summary>
    /// Represents the response model for weather information.
    /// </summary>
    public class WeatherResponse
    {

        public Location Location { get; set; }


        public Current Current { get; set; }

        public AstronomyResponse Astronomy { get; set; }
    }


    public class AstronomyResponse
    {
        public Astro Astro { get; set; }
    }

    /// <summary>
    /// For Astro information from the API
    /// </summary>
    public class Astro
    {
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public string Moonrise { get; set; }
        public string Moonset { get; set; }

        [JsonProperty("moon_phase")]
        public string MoonPhase { get; set; }
    }

    /// <summary>
    /// Represents the location information in the weather response.
    /// </summary>
    public class Location
    {

        public string Name { get; set; }


        public string Region { get; set; }


        public string Country { get; set; }
    }

    /// <summary>
    /// Represents the weather condition.
    /// </summary>
    public class Condition
    {

        public string Text { get; set; }
    }

    /// <summary>
    /// Represents the current weather information.
    /// </summary>
    public class Current
    {

        [JsonProperty("temp_c")]
        public double TempC { get; set; }


        [JsonProperty("wind_kph")]
        public double WindKph { get; set; }


        [JsonProperty("condition")]
        public Condition Condition { get; set; }
    }

}
