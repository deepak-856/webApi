using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wrenchwise.Interfaces;
using wrenchwise.Models;
using wrenchwise.Services;

namespace wrenchwise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly RegisterService _registerService;
        private readonly ITechnicianService _technicianService;
        //private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private int loginId;
        

        //public AdminController(RegisterService registerService)
        //{
        //    _registerService = registerService;
        //}

        //public AdminController(ITechnicianService technicianService)
        //{
        //    _technicianService = technicianService;
        //}
        public AdminController(RegisterService registerService, ITechnicianService technicianService, IUserService userService)
        {
            _registerService = registerService;
            _technicianService = technicianService;
            //_adminService = adminService;
            _userService = userService;
        }


        //[Authorize(Roles = "Admin")]
        [Authorize(Roles = "Admin")] // Only Admins
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _registerService.GetAllRegisteredUsersAsync();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")] // Only Admins
        [HttpPost("add-technicians")]
        public async Task<IActionResult> AddTechnician([FromBody] TechnicianRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _technicianService.AddTechnicianAsync(request);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize(Roles = "Admin")] // Only Admins
        [HttpGet("all-technicians")]
        public async Task<IActionResult> GetAllTechnicians()
        {
            var technicians = await _technicianService.GetAllRegisteredTechniciansAsync();
            return Ok(technicians);
        }

        [Authorize(Roles = "Admin")] // Only Admins
        [HttpPut("update-tech/{id}")]
        public async Task<IActionResult> UpdateTechnicians( int id, [FromBody] TechnicianRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _technicianService.UpdateTechnicianAsync(id, request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "Admin")] // Only Admins
        [HttpDelete("delete-technician/{loginId}")]
        public async Task<IActionResult> DeleteTechnician(int loginId)
        {
            var result = await _technicianService.DeleteTechnicianAsync(loginId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "Admin")] // Only Admins
        [HttpGet("user-profile/{loginId}")]
        public async Task<IActionResult> GetUserProfileByLoginId(int loginId)
        {
            if (loginId <= 0)
                return BadRequest("Invalid login ID.");

            var user = await _userService.GetUserByLoginIdAsync(loginId);

            if (user == null)
                return NotFound(new { Success = false, Message = "User not found." });

            return Ok(new { Success = true, Message = "User profile retrieved successfully.", Data = user });
        }

       

    }
}
