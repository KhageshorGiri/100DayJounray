using Microsoft.Extensions.Hosting;

namespace BackGrounds.JOBS.BackgroundServices
{
    public class TestbackgroundJobs : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
