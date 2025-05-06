using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;

namespace Keeper_AuthService.Models.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public RoleDTO Role { get; set; } = null!;
        public ProfileDTO Profile { get; set; } = null!;
    }
}
