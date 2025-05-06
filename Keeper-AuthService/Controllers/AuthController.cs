using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Keeper_AuthService.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            ServiceResponse<object?> response = await _authService.Register(registerDTO); 
            return HandleServiceResponse(response);
        }

        [HttpPost("activate")]
        public async Task<IActionResult> Activation([FromBody] ActivationDTO activation)
        {
            ServiceResponse<object?> response = await _authService.Activation(activation);
            return HandleServiceResponse(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            ServiceResponse<SessionDTO?> response = await _authService.Login(login);
            return HandleServiceResponse(response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            string? userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdString == null || !Guid.TryParse(userIdString, out Guid userId))
                return Unauthorized(new { message = "Invalid token: user ID missing or malformed." });

            ServiceResponse<object?> serviceResponse = await _authService.Logout(userId);
            return HandleServiceResponse(serviceResponse);
        }


        [HttpPost("updatejwt")]
        public async Task<IActionResult> UpdateJwt([FromBody] UpdateJwtDTO updateJwt)
        {
            ServiceResponse<string?> response = await _authService.UpdateJwt(updateJwt);
            return HandleServiceResponse(response);
        }

        private IActionResult HandleServiceResponse<T>(ServiceResponse<T> response)
        {
            if (!response.IsSuccess)
                return StatusCode(statusCode: response.Status, new { message = response.Message });

            return StatusCode(statusCode: response.Status, new { data = response.Data, message = response.Message });
        }
    }
}
