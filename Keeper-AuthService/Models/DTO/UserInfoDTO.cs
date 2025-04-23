using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.DTO
{
    public class UserInfoDTO
    {
        public Guid UserId { get; set; }

        public ProfileDTO Profile { get; set; }

        public TokensDTO Tokens { get; set; }
    }
}
