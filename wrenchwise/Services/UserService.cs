using Dapper;
using wrenchwise.Interfaces;
using wrenchwise.Models;
using wrenchwise.Utility;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace wrenchwise.Services
{
    public class UserService : IUserService
    {
        private readonly DBGateway _dbGateway;

        public UserService(DBGateway dbGateway)
        {
            _dbGateway = dbGateway;
        }

        public async Task<IActionResult> UpdateProfileAsync(UpdateUserprofile request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_login_id", request.LoginId);
            parameters.Add("p_email", request.Email);
            parameters.Add("p_mobile", request.Mobile);
            parameters.Add("p_address", request.Address);

            try
            {
                // Step 1: Execute the stored procedure to update the profile
                await _dbGateway.ExecuteSPAsync("sp_update_user_profile", parameters);

                // Step 2: Fetch the updated profile
                var fetchParams = new DynamicParameters();
                fetchParams.Add("p_login_id", request.LoginId);

                var updatedProfile = await _dbGateway.QuerySPAsync<UserProfile>("sp_get_user_profile", fetchParams);

                if (updatedProfile == null || !updatedProfile.Any())
                {
                    return ErrorResponse("Profile update succeeded, but fetching updated profile failed.");
                }

                // Return success with the updated profile
                return SuccessResponse("Profile updated successfully.", updatedProfile.First());
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Profile update failed: {ex.Message}");
            }
        }

        // Static methods for responses directly in the service
        private IActionResult SuccessResponse(string message, object? data = null)
        {
            return new JsonResult(new { Success = true, Message = message, Data = data });
        }

        private IActionResult ErrorResponse(string message)
        {
            return new JsonResult(new { Success = false, Message = message });
        }
    }
}
