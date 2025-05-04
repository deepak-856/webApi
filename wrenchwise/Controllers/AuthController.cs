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
        private readonly IUserService _userService; 

        public AuthController(IRegisterService registerService, IUserService userService)
        {
            _registerService = registerService;
            _userService = userService;
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


        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserprofile request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _userService.UpdateProfileAsync(request);
        }




    }
}
