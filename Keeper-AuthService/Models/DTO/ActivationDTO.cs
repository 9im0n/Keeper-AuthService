using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.DTO
{
    public class ActivationDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;


        [Required]
        public string ActivationPassword { get; set; } = null!;
    }
}
