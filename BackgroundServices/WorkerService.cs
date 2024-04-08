using BackgroundServiceAPI.BackgroundTasks;

namespace BackgroundServiceAPI.BackgroundServices
{
    public class WorkerService : BackgroundService
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly ICountEmployeeDataTask _employeeDataProcessor;

        public WorkerService(ILogger<WorkerService> logger, ICountEmployeeDataTask employeeDataProcessor)
        {
            _logger = logger;
            _employeeDataProcessor = employeeDataProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await _employeeDataProcessor.CountEmployeeDataAsync(stoppingToken);
                }
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while executing the background task.");
            }
        }
    }
}
