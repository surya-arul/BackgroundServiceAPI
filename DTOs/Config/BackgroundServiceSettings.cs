using System.ComponentModel.DataAnnotations;

namespace BackgroundServiceAPI.Models.Config
{
    public class BackgroundServiceSettings
    {
        [Range(0.1, 1440)]
        public double IntervalInMinutes { get; set; }

        [Required]
        public string FilePath { get; set; } = null!;
    }
}
