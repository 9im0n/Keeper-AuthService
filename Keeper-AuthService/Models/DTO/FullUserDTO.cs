using System.Globalization;

namespace Keeper_AuthService.Models.DTO
{
    public class FullUserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public RoleDTO Role { get; set; } = null!;
        public ProfileDTO Profile { get; set; } = null!;
    }
}
