using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.DTO
{
    public class UpdateJwtDTO
    {
        [Required]
        public string RefreshToken { get; set; } = null!;
    }
}
