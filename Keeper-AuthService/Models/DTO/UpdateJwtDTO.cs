using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.DTO
{
    public class UpdateJwtDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
