namespace Keeper_AuthService.Models.DTO
{
    public class UserInfoDTO
    {
        public Guid Id { get; set; }

        public required string Email { get; set; }

        public string Role { get; set; }

        public ProfileDTO Profile { get; set; }
    }
}
