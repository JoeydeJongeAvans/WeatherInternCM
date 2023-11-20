namespace WeatherCM.Services
{
    public interface ISendSMSService
    {
        void SendCurrentWeatherSMS(string message);

        void SendAstronomySMS(string message);
    }
}
