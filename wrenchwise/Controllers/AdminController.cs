using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wrenchwise.Services;

namespace wrenchwise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly RegisterService _registerService;

        public AdminController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _registerService.GetAllRegisteredUsersAsync();
            return Ok(users);
        }
    }

}
