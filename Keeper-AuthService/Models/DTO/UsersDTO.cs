using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;

namespace Keeper_AuthService.Models.DTO
{
    public class UsersDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public bool IsActive { get; set; } = false;

        public required Guid RoleId { get; set; }
        public virtual RolesDTO Role { get; set; }

        public ProfileDTO Profile { get; set; }

        public virtual ICollection<PermissionsDTO> Permissions { get; set; }
    }
}
