using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Keeper_AuthService.Models.DB
{
    [Table("PendingActivations")]
    public class PendingActivation : BaseModel
    {
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public string ActivationCodeHash { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow + TimeSpan.FromDays(30);
    }
}
