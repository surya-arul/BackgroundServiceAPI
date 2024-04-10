using BackgroundServiceAPI.BackgroundTasks;
using BackgroundServiceAPI.Models.Config;
using Microsoft.Extensions.Options;

namespace BackgroundServiceAPI.BackgroundServices
{
    public class WorkerService : BackgroundService
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly ICountEmployeeDataJob _countEmployeeDataJob;
        private readonly IOptionsMonitor<BackgroundServiceSettings> _bgSettings;

        public WorkerService(ILogger<WorkerService> logger, ICountEmployeeDataJob countEmployeeDataJob, IOptionsMonitor<BackgroundServiceSettings> bgSettings)
        {
            _logger = logger;
            _countEmployeeDataJob = countEmployeeDataJob;
            _bgSettings = bgSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await _countEmployeeDataJob.CountEmployeeDataAsync(_bgSettings.CurrentValue.FilePath);

                    await Task.Delay(TimeSpan.FromMinutes(_bgSettings.CurrentValue.IntervalInMinutes), stoppingToken);
                }
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while executing the background task.");
            }
        }
    }
}
