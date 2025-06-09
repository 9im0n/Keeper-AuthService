using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Keeper_AuthService.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO newUser)
        {
            try
            {
                ServiceResponse<UsersDTO?> response = await _authService.Registration(newUser);
                
                if (!response.IsSuccess)
                    return StatusCode(statusCode: response.Status, new { message = response.Message });

                return Created();
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, detail: ex.Message);
            }
        }
    }
}
