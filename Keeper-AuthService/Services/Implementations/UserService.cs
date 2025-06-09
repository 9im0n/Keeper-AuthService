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

        
        public async Task<ServiceResponse<UserDTO?>> CreateAsync(CreateUserDTO createUserDTO)
        {
            return await _httpClientService.PostAsync<CreateUserDTO, UserDTO?>($"{_api}/users", createUserDTO);
        }


        public async Task<ServiceResponse<UserDTO?>> GetByIdAsync(Guid Id)
        {
            return await _httpClientService.GetAsync<UserDTO?>($"{_api}/users/{Id}");
        }

        public async Task<ServiceResponse<UserDTO?>> ActivateUser(ActivationDTO activation)
        {
            return await _httpClientService.PostAsync<ActivationDTO, UserDTO?>($"{_api}/users/activate", activation);
        }


        public async Task<ServiceResponse<UserDTO?>> GetByEmailAsync(string email)
        {
            return await _httpClientService.GetAsync<UserDTO?>($"{_api}/users/by-email/{email}");
        }

        public async Task<ServiceResponse<FullUserDTO?>> GetFullUserByEmailAsync(string email)
        {
            return await _httpClientService.GetAsync<FullUserDTO?>($"{_api}/users/by-email/{email}/full");
        }
    }
}
