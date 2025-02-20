using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.DTO
{
    public class UserActivationDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        public string ActivationPassword { get; set; }
    }
}
