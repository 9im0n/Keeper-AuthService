using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.DTO
{
    public class CreatePendingActivationDTO
    {
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string ActivationCodeHash { get; set; } = null!;
    }
}
