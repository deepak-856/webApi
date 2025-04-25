using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using wrenchwise.Interfaces;
using wrenchwise.Models;

namespace wrenchwise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("admin")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginRequest request)
        {
            request.role_type = 1;
            var result = await _loginService.Authenticate(request);

            return result.Success ? Ok(result) : Unauthorized(result);
        }

        [HttpPost("user")]
        public async Task<IActionResult> UserLogin([FromBody] LoginRequest request)
        {
            request.role_type = 2;
            var result = await _loginService.Authenticate(request);

            return result.Success ? Ok(result) : Unauthorized(result);
        }

        [HttpPost("technician")]
        public async Task<IActionResult> TechnicianLogin([FromBody] LoginRequest request)
        {
            request.role_type = 3;
            var result = await _loginService.Authenticate(request);

            return result.Success ? Ok(result) : Unauthorized(result);
        }

        //// ✅ JWT protected endpoint
        //[Authorize]
        //[HttpGet("secure")]
        //public IActionResult SecureData()
        //{
        //    var user = HttpContext.User.Identity?.Name;
        //    var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        //    return Ok(new
        //    {
        //        Message = "You have accessed a protected endpoint!",
        //        User = user,
        //        Role = role
        //    });
        //}

        [HttpPost("technicians")]
        public async Task<IActionResult> TechniciansLogin([FromBody] LoginRequest request)
        {
            request.role_type = 3;
            var result = await _loginService.Authenticate(request);

            return result.Success ? Ok(result) : Unauthorized(result);
        }
    }
}
