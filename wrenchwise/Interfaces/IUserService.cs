using Microsoft.AspNetCore.Mvc;
using wrenchwise.Models;

namespace wrenchwise.Interfaces
{
    public interface IUserService
    {
        Task<IActionResult> UpdateProfileAsync(UpdateUserprofile request);
    }

   
}
