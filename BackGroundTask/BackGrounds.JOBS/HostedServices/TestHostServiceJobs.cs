using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackGrounds.JOBS.HostedServices
{
    public class TestHostServiceJobs : IHostedService, IDisposable
    {
        private readonly ILogger<TestHostServiceJobs> _logger;

        public TestHostServiceJobs(ILogger<TestHostServiceJobs> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hosted Sevice Started. {0}", DateTime.Now);

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hosted Sevice Stoped.");
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
