using Microsoft.Extensions.Hosting;

namespace BackGrounds.JOBS.HostedServices
{
    internal class ExampleHostedLifeCycleService : IHostedLifecycleService
    {
        public Task StartingAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StartedAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }



        public Task StoppingAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StoppedAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

       
    }
}
