using Dapper;
using wrenchwise.Interfaces;
using wrenchwise.Models;
using wrenchwise.Utility;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace wrenchwise.Services
{
    public class AdminService
    {
        //    private readonly DBGateway _dbGateway;

        //    public AdminService(DBGateway dbGateway)
        //    {
        //        _dbGateway = dbGateway;
        //    }

        //    public async Task<UserProfile?> GetUserProfileAsync(int loginId)
        //    {
        //        var fetchParams = new DynamicParameters();
        //        fetchParams.Add("p_login_id", loginId);

        //        var result = await _dbGateway.QuerySPAsync<UserProfile>("sp_get_user_profile", fetchParams);
        //        return result.FirstOrDefault();
        //    }


        //    // Shared response methods
        //    private IActionResult SuccessResponse(string message, object? data = null)
        //    {
        //        return new JsonResult(new { Success = true, Message = message, Data = data });
        //    }

        //    private IActionResult ErrorResponse(string message)
        //    {
        //        return new JsonResult(new { Success = false, Message = message });
        //    }


    }
}
