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

        //public AdminController(RegisterService registerService)
        //{
        //    _registerService = registerService;
        //}

        //public AdminController(ITechnicianService technicianService)
        //{
        //    _technicianService = technicianService;
        //}
        public AdminController(RegisterService registerService, ITechnicianService technicianService)
        {
            _registerService = registerService;
            _technicianService = technicianService;
        }


        //[Authorize(Roles = "Admin")]
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _registerService.GetAllRegisteredUsersAsync();
            return Ok(users);
        }

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

        [HttpGet("all-technicians")]
        public async Task<IActionResult> GetAllTechnicians()
        {
            var technicians = await _technicianService.GetAllRegisteredTechniciansAsync();
            return Ok(technicians);
        }
    }
}
