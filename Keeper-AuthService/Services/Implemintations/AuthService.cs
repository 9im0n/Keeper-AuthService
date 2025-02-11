using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.Db;
using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Services.Interfaces;

namespace Keeper_AuthService.Services.Implemintations
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IActivationPasswordsService _activationPasswordsService;

        public AuthService(IUserService userService, IActivationPasswordsService activationPasswordsService)
        {
            _userService = userService;
            _activationPasswordsService = activationPasswordsService;
        }

        public async Task<ServiceResponse<ActivationPasswords?>> Registration(CreateUserDTO newUser)
        {
            ServiceResponse<bool> createUserResponse = await _userService.CreateAsync(newUser);

            if (!createUserResponse.IsSuccess)
                return ServiceResponse<ActivationPasswords?>.Fail(null, createUserResponse.Status, createUserResponse.Message);

            ServiceResponse<ActivationPasswords> activationPwResponse = await _activationPasswordsService.CreateAsync(newUser.Email);
            
            if (!activationPwResponse.IsSuccess)
                return ServiceResponse<ActivationPasswords?>.Fail(null, activationPwResponse.Status, activationPwResponse.Message);

            // TODO: Сделать отправку сообщения на email.

            return ServiceResponse<ActivationPasswords>.Success(activationPwResponse.Data, 201, activationPwResponse.Message);
        }
    }
}
