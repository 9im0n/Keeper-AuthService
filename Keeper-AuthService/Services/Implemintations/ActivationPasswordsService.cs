using Keeper_AuthService.Models.Db;
using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Repositories.Implemintations;
using Keeper_AuthService.Repositories.Interfaces;
using Keeper_AuthService.Services.Interfaces;

namespace Keeper_AuthService.Services.Implemintations
{
    public class ActivationPasswordsService : IActivationPasswordsService
    {
        private readonly IActivationPasswordsRepository _repository;

        public ActivationPasswordsService(IActivationPasswordsRepository activationPasswordsRepository)
        {
            _repository = activationPasswordsRepository;
        }

        public async Task<ServiceResponse<ActivationPasswords>> CreateAsync(string email)
        {
            string password = "";
            Random rnd = new Random();

            for (int i = 0; i < 6; i++)
            {
                password += rnd.Next(0, 10).ToString();
            }

            ActivationPasswords activationPassword = new ActivationPasswords()
            {
                Email = email,
                Password = password
            };

            ActivationPasswords newPassword = await _repository.CreateAsync(activationPassword);

            return ServiceResponse<ActivationPasswords>.Success(activationPassword);
        }
    }
}
