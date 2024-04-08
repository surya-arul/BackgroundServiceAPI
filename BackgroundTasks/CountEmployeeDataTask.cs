using BackgroundServiceAPI.Models.Config;
using BackgroundServiceAPI.Repositories;
using Microsoft.Extensions.Options;

namespace BackgroundServiceAPI.BackgroundTasks
{
    public interface ICountEmployeeDataTask
    {
        Task CountEmployeeDataAsync(CancellationToken stoppingToken);
    }

    public class CountEmployeeDataTask : ICountEmployeeDataTask
    {
        private readonly ILogger<CountEmployeeDataTask> _logger;
        private readonly IOptionsMonitor<BackgroundServiceSettings> _bgSettings;
        private readonly IServiceProvider _serviceProvider;

        public CountEmployeeDataTask(ILogger<CountEmployeeDataTask> logger, IOptionsMonitor<BackgroundServiceSettings> bgSettings, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _bgSettings = bgSettings;
            _serviceProvider = serviceProvider;
        }

        public async Task CountEmployeeDataAsync(CancellationToken stoppingToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var employeeRepository = scope.ServiceProvider.GetRequiredService<IEmployeeRepository>();

                    // Ensure the database table exists before writing the file
                    if (await employeeRepository.IsTableExists())
                    {
                        var employeeCount = await employeeRepository.GetEmployeesCount();

                        var directoryPath = Path.GetDirectoryName(_bgSettings.CurrentValue.FilePath);

                        // Ensure the directory exists before writing the file
                        if (Directory.Exists(directoryPath))
                        {
                            await File.WriteAllTextAsync(_bgSettings.CurrentValue.FilePath, $"Current count of employees: {employeeCount} - [{DateTime.Now}]");
                        }
                        else
                        {
                            _logger.LogWarning($"Directory '{directoryPath}' does not exist. Skipping file writing operation.");
                        }
                    }
                    else
                    {
                        _logger.LogWarning("Table does not exist. Skipping file writing operation.");
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(_bgSettings.CurrentValue.IntervalInMinutes), stoppingToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
