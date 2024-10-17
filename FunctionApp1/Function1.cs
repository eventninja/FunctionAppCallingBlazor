using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public Function1(ILoggerFactory loggerFactory, IHttpClientFactory httpClientFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _httpClientFactory = httpClientFactory;
        }

        [Function("Function1")]
        public async Task Run([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }

            var message = $"The time is {DateTime.Now}";

            var httpClient = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7264/api/broadcast?message={message}");
            await httpClient.SendAsync(httpRequestMessage);
        }
    }
}
