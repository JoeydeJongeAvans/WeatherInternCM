using CM.Text;

namespace WeatherCM.Services
{
    public class SendSMSService : ISendSMSService
    {
        private readonly ILogger<SendSMSService> _logger;
        private readonly TextClient _textClient;
        private readonly IConfiguration _configuration;

        public SendSMSService(ILogger<SendSMSService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration; 
            _textClient = new TextClient(new Guid(_configuration["CMTextApiKey"]));
        }

        public void SendAstronomySMS(string message)
        {
            // Replace with your sender name, recipient phone number, and reference
            var senderName = "Weather Joey";
            var recipientPhoneNumber = "0031627206897";
            var reference = "Weather";

            var result = _textClient.SendMessageAsync(message, senderName, new List<string> { recipientPhoneNumber }, reference).Result;

            _logger.LogInformation($"CM: {result}");
        }

        public void SendCurrentWeatherSMS(string message)
        {
            // Replace with your sender name, recipient phone number, and reference
            var senderName = "Weather Joey";
            var recipientPhoneNumber = "0031627206897";
            var reference = "Weather";

            var result = _textClient.SendMessageAsync(message, senderName, new List<string> { recipientPhoneNumber }, reference).Result;

            _logger.LogInformation($"CM: {result}");
        }

        
    }
}
