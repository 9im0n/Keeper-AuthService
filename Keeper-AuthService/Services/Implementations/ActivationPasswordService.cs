using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Keeper_AuthService.Services.Implementations
{
    public class ActivationPasswordService : IActivationPasswordService
    {
        public ServiceResponse<ActivationPasswordDTO> Generate()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder stringBuilder = new StringBuilder();
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] data = new byte[1];
                for (int i = 0; i < 6; i++)
                {
                    rng.GetBytes(data);
                    int index = data[0] % chars.Length;
                    stringBuilder.Append(chars[index]);
                }
            }

            string password = stringBuilder.ToString();
            ActivationPasswordDTO dto = new ActivationPasswordDTO { Password = password };
            return ServiceResponse<ActivationPasswordDTO>.Success(dto);
        }
    }
}
