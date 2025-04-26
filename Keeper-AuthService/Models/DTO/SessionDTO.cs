using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Keeper_AuthService.Models.DTO
{
    public class SessionDTO
    {
        public Guid Id { get; set; }

        public required string Email { get; set; }

        public string Role { get; set; }

        public ProfileDTO Profile { get; set; }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
