using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Keeper_AuthService.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _api;

        public UserService(IHttpClientService httpClientService, IOptions<ApiUrls> api)
        {
            _httpClientService = httpClientService;
            _api = api.Value.UserService;
        }

        
        public async Task<ServiceResponse<UsersDTO?>> CreateAsync(CreateUserDTO newUser)
        {
            return await _httpClientService.PostAsync<CreateUserDTO, UsersDTO?>($"{_api}/users/registration", newUser);
        }


        public async Task<ServiceResponse<UsersDTO?>> ActivateUser(UserActivationDTO activation)
        {
            return await _httpClientService.PostAsync<UserActivationDTO, UsersDTO?>($"{_api}/users/activate", activation);
        }


        public async Task<ServiceResponse<UsersDTO?>> GetByEmailAsync(string email)
        {
            return await _httpClientService.GetAsync<UsersDTO?>($"{_api}/users/{email}");
        }
    }
}
