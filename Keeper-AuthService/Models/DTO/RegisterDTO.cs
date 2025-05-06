using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.DTO
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = null!;

        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
