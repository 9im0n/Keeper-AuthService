using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public async Task<IActionResult> Register([FromBody] RegisterDTO newUser)
        {
            
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            
        }


        [HttpPost("logout/{id:guid}")]
        public async Task<IActionResult> Logout(Guid id)
        {
            
        }


        [HttpPost("activate")]
        public async Task<IActionResult> Activation([FromBody] ActivationDTO activation)
        {
            
        }


        [HttpPost("jwt/update")]
        public async Task<IActionResult> UpdateJwt([FromBody] UpdateJwtDTO updateJwt)
        {
            
        }

        private IActionResult HandleServiceResponse<T>(ServiceResponse<T> response)
        {
            if (!response.IsSuccess)
                return StatusCode(statusCode: response.Status, new { message = response.Message });

            return StatusCode(statusCode: response.Status, new { message = response.Message });
        }
    }
}
