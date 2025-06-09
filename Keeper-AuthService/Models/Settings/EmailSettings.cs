namespace Keeper_AuthService.Models.Settings
{
    public class EmailSettings
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string SmtpHost { get; set; } = "smtp.gmail.com";
        public int SmtpPort { get; set; } = 587;
    }
}
