using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.DTO
{
    public class PendingActivationDTO
    {
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string ActivationCodeHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow + TimeSpan.FromDays(30);
    }
}
