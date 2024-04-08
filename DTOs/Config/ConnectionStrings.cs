using System.ComponentModel.DataAnnotations;

namespace BackgroundServiceAPI.DTOs.Config
{
    public class ConnectionStrings
    {
        [Required]
        public string DbConnection { get; set; } = null!;
    }
}
