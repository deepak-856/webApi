using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wrenchwise.Interfaces;
using wrenchwise.Models;
using wrenchwise.Models.Booking;
using wrenchwise.Services;

namespace wrenchwise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly IUserService _userService;
        private readonly IVehicleService _vehicleService;

        public AuthController(IRegisterService registerService, IUserService userService, IVehicleService vehicleService)
        {
            _registerService = registerService;
            _userService = userService;
            _vehicleService = vehicleService;
        }

        [Authorize(Roles = "User")] // Only user
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

        [Authorize(Roles = "User")] // Only user
        [HttpPost("update-user")]
        public async Task<IActionResult> GetUserProfileAsync([FromBody] UpdateUserprofile request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _userService.UpdateProfileAsync(request);
        }

        // Route: GET /api/user/profile/{loginId}
        [Authorize(Roles = "User")] // Only user
        [HttpGet("update-user/profile/{loginId}")]
        public async Task<IActionResult> GetUserProfileAsync(int loginId)
        {
            if (loginId <= 0)
                return BadRequest("Invalid login ID.");

            var user = await _userService.GetUserByLoginIdAsync(loginId);

            if (user == null)
                return NotFound(new { Success = false, Message = "User not found." });

            return Ok(new { Success = true, Message = "User profile retrieved successfully.", Data = user });
        }

        //[HttpPost("vehicle")]
        //public async Task<IActionResult> AddVehicle([FromBody] Vehicle vehicle)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return await _vehicleService.AddVehicleAsync(vehicle);
        //}

        [Authorize(Roles = "User")] // Only user
        [HttpPost("add-vehicle-booking")]
        public async Task<IActionResult> AddVehicleBooking([FromBody] VehicleBookingRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage);
                return BadRequest(new { Success = false, Errors = errors });
            }

            return await _vehicleService.AddVehicleWithBookingAsync(request.Vehicle, request.Booking);
        }

    }
}

