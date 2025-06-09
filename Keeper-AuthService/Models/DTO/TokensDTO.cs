namespace Keeper_AuthService.Models.DTO
{
    public class TokensDTO
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
