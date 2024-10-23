using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.CrossCutting.Health
{
    public class ApiHealthCheck : IHealthCheck
    {
        private ILogger Logger { get; }

        public ApiHealthCheck(
              ILogger logger)
        {
            Logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            Logger.LogInformation("Health Check Executing");

            return await Task.FromResult(HealthCheckResult.Healthy("UP"));
        }

    }
}
