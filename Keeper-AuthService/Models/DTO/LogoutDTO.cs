using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.DTO
{
    public class LogoutDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
