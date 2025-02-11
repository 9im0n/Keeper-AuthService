using Keeper_AuthService.Services.Interfaces;

namespace Keeper_AuthService.Services.Implemintations
{
    public class UserService : IUserService
    {
        private readonly IHttpClientService _httpClientService;

        public UserService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }


    }
}
