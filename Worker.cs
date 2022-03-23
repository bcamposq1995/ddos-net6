using Microsoft.Extensions.Logging;

namespace DDOS
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    var response = await httpClient.GetStringAsync(Environment.GetEnvironmentVariable("URL"));
                    if(response != null)
                        this._logger.LogInformation(response);
                }
                catch (Exception ex)
                {
                    this._logger.LogError(exception: ex, message: "Error");
                }
            }
        }
    }
}