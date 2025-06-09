using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Keeper_AuthService.Models.DTO
{
    public class SessionDTO
    {
        public UserDTO User { get; set; } = null!;
        public TokensDTO Tokens { get; set; } = null!;
    }
}
