using BackgroundServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _configService;

        public ConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        [HttpGet]
        [Route("GetBackgroundServiceConfig")]
        public IActionResult GetBackgroundServiceConfig()
        {
            var response = _configService.GetBackgroundServiceConfig();
            return Ok(response);
        }
    }
}
