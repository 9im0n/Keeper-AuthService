using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.DB
{
    public class RefreshTokens : BaseModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Token { get; set; } = null!;

        [Required]
        public DateTime ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool Revoked { get; set; } = false;
    }
}
