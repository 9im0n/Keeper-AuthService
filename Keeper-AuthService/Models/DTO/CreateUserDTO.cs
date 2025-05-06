namespace Keeper_AuthService.Models.DTO
{
    public class CreateUserDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
