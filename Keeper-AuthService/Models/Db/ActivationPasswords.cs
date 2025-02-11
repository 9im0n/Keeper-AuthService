using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.Db
{
    public class ActivationPasswords : BaseModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
