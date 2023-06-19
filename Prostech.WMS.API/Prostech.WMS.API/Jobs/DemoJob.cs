using Microsoft.AspNetCore.Mvc;
using Quartz;

namespace Prostech.WMS.API.Jobs
{
    [DisallowConcurrentExecution]
    public class DemoJob : IJob
    {
        private readonly ILogger<DemoJob> _logger;
        public DemoJob(ILogger<DemoJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello world!");
            return Task.CompletedTask;
        }
    }
}
