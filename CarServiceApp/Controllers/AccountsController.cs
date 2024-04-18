using CarServiceApp.DTO;
using CarServiceApp.Services.Contracts;
using CarServiceApp.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace CarServiceApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            var response = await _authService.CreateAsync(registerDto);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var response = await _authService.SignInAsync(loginDto);
            if (!response.Success)
            {
                return Unauthorized(response.Message);
            }

            return Ok(new { Token = response.Token });
        }
    }
}