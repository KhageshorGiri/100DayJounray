using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackGrounds.JOBS.HostedServices
{
    public class ExampleHostedLifeCycleService : IHostedLifecycleService
    {
        private readonly ILogger<ExampleHostedLifeCycleService> _logger;

        public ExampleHostedLifeCycleService(ILogger<ExampleHostedLifeCycleService> logger)
        {
            _logger = logger;
        }

        public async Task StartingAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(5000);
            _logger.LogInformation("Starting IHosted Service");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start IHosted Service");
        }

        public async Task StartedAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started IHosted Service");
        }


        public async Task StoppingAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopinging IHosted Service");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stop IHosted Service");
        }

        public async Task StoppedAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopped IHosted Service");
        }

       
    }
}
