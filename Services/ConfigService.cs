using BackgroundServiceAPI.Models.Config;
using BackgroundServiceAPI.Models.Response;
using Microsoft.Extensions.Options;

namespace BackgroundServiceAPI.Services
{
    public interface IConfigService
    {
        GetBackgroundServiceSettingsResponse GetBackgroundServiceConfig();
    }

    public class ConfigService : IConfigService
    {
        private readonly IOptionsMonitor<BackgroundServiceSettings> _options;

        public ConfigService(IOptionsMonitor<BackgroundServiceSettings> options)
        {
            _options = options;
        }

        public GetBackgroundServiceSettingsResponse GetBackgroundServiceConfig()
        {
            try
            {
                var bgServiceSettingsResponse = new GetBackgroundServiceSettingsResponse
                {
                    IntervalInMinutes = _options.CurrentValue.IntervalInMinutes,
                    FilePath = _options.CurrentValue.FilePath,
                };

                return bgServiceSettingsResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
