using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ClientPro.API.HealthCheck
{
    public class ApplicationHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                // Perform your application health checks here

                // For example, check if your application's critical components are functioning correctly.

                // If everything is fine, return Healthy status
                return Task.FromResult(HealthCheckResult.Healthy("Application is healthy."));
            }
            catch (Exception ex)
            {
                // If an exception occurs during the health check, return Unhealthy status
                return Task.FromResult(HealthCheckResult.Unhealthy("An error occurred while checking application health.", ex));
            }
        }
    }
}
