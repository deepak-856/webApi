using Microsoft.AspNetCore.Mvc;
using wrenchwise.Interfaces;
using wrenchwise.Models;

namespace wrenchwise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public AuthController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _registerService.RegisterAsync(request);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
