using Microsoft.AspNetCore.Mvc;
using wrenchwise.Models.Booking;

namespace wrenchwise.Interfaces
{
    public interface IVehicleService
    {
        Task<IActionResult> AddVehicleWithBookingAsync(Vehicle vehicle, Booking? booking);
    }
}
