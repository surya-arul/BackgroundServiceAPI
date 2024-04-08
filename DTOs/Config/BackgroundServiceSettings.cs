using System.ComponentModel.DataAnnotations;

namespace BackgroundServiceAPI.Models.Config
{
    public class BackgroundServiceSettings
    {
        [Range(1, 1440)]
        public int IntervalInMinutes { get; set; }
    }
}
