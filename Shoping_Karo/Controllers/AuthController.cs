using Microsoft.AspNetCore.Mvc;
using Shoping_Karo.DTOs;
using Shoping_Karo.Models;
using Shoping_Karo.Server.Helper;
using Shoping_Karo.Services.Interface;

namespace Shoping_Karo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly JwtService _jwtService;

        public AuthController(IAuth authService, ILogger<AuthController> logger, JwtService jwtService)
        {
            _authService = authService;
            _logger = logger;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] users user)
        {
            if (user == null) return BadRequest("Invalid user data");
            _authService.Register(user);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _authService.login(dto);
            if (user == null) return Unauthorized(new { message = "Invalid email or password" });

            var token = _jwtService.GenerateToken(user);
            return Ok(new { message = "Login successful", user, token });
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] users user)
        {
            if (user == null) return BadRequest("Invalid user data");
            _authService.UpdateUser(user);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _authService.DeleteUser(id);
            return Ok(new { message = "User deleted successfully" });
        }
    }
}