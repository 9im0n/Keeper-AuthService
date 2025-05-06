using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Keeper_AuthService.Models.DB
{
    [Table("RefreshTokens")]
    public class RefreshToken : BaseModel
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
