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
                return Problem(statusCode: 500, detail: $"Auth Servcie: {ex.Message}");
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                ServiceResponse<TokensDTO?> response = await _authService.Login(login);

                if (!response.IsSuccess)
                    return StatusCode(statusCode: response.Status, new { message = response.Message });

                return Ok(new { data = response.Data, message = response.Message });
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, detail: $"Auth Servcie: {ex.Message}");
            }
        }


        [HttpPost("activation")]
        public async Task<IActionResult> Activation([FromBody] UserActivationDTO activation)
        {
            try
            {
                ServiceResponse<UsersDTO?> response = await _authService.UserActivation(activation);

                if (!response.IsSuccess)
                    return StatusCode(statusCode: response.Status, new { message = response.Message });

                return StatusCode(statusCode: response.Status, new { message = response.Message });
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, detail: $"Auth Servcie: {ex.Message}");
            }
        }


        [HttpPost("jwt/update")]
        public async Task<IActionResult> UpdateJwt([FromBody] UpdateJwtDTO updateJwt)
        {
            try
            {
                ServiceResponse<string?> response = await _authService.UpdateJwt(updateJwt);

                if (!response.IsSuccess)
                    return StatusCode(statusCode: response.Status, new { message = response.Message });

                return Ok(new { data = response.Data, message = response.Message });
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, detail: $"Auth Servcie: {ex.Message}");
            }
        }
    }
}
