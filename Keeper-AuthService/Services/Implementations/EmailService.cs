using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.Settings;
using Keeper_AuthService.Services.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace Keeper_AuthService.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<ServiceResponse<object?>> SendWelcomeEmailAsync(string email, ActivationPasswordDTO password)
        {
            var message = new MimeMessage();

            Console.WriteLine(_emailSettings.Email);
            Console.WriteLine(_emailSettings.Password);
            try
            {
                message.From.Add(new MailboxAddress("Keeper", _emailSettings.Email));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Welcome2Keeper!";
                message.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = $"Your activation code: {password.Password}"
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.SmtpHost, _emailSettings.SmtpPort, false);
                    await client.AuthenticateAsync(_emailSettings.Email, _emailSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return ServiceResponse<object?>.Success(default);
            }
            catch (Exception ex)
            {
                return ServiceResponse<object?>.Fail(default, 400, $"EmailService: {ex.Message}");
            }
        }
    }
}
