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

        
        public async Task<ServiceResponse<UserDTO?>> CreateAsync(RegisterDTO newUser)
        {
            return await _httpClientService.PostAsync<RegisterDTO, UserDTO?>($"{_api}/users/registration", newUser);
        }


        public async Task<ServiceResponse<UserDTO?>> ActivateUser(ActivationDTO activation)
        {
            return await _httpClientService.PostAsync<ActivationDTO, UserDTO?>($"{_api}/users/activate", activation);
        }


        public async Task<ServiceResponse<UserDTO?>> GetByEmailAsync(string email)
        {
            return await _httpClientService.GetAsync<UserDTO?>($"{_api}/users/{email}");
        }
    }
}
