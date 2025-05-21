using Dapper;
using Microsoft.AspNetCore.Mvc;
using wrenchwise.Interfaces;
using wrenchwise.Models.Booking;
using wrenchwise.Utility;

namespace wrenchwise.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly DBGateway _dbGateway;

        public VehicleService(DBGateway dbGateway)
        {
            _dbGateway = dbGateway;
        }

        public async Task<IActionResult> AddVehicleWithBookingAsync(Vehicle vehicle, Booking booking)
        {
            using var connection = _dbGateway.Connection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // 1. Insert vehicle
                var insertVehicleQuery = @"
                    INSERT INTO vehicles 
                        (brand, model, registration_number, engine_number, chassis_number, manufacture_date)
                    VALUES 
                        (@Brand, @Model, @RegistrationNumber, @EngineNumber, @ChassisNumber, @ManufactureDate);
                    SELECT LAST_INSERT_ID();";

                var vehicleId = await connection.ExecuteScalarAsync<int>(
                    insertVehicleQuery, vehicle, transaction);

                // 2. Insert booking with the vehicleId
                var insertBookingQuery = @"
                    INSERT INTO bookings 
                        (vehicle_id, booking_date, service_type, pick_drop, status, remarks, created_at)
                    VALUES 
                        (@VehicleId, @BookingDate, @ServiceType, @PickDrop, @Status, @Remarks, @CreatedAt);";

                var bookingParams = new DynamicParameters();
                bookingParams.Add("@VehicleId", vehicleId);
                bookingParams.Add("@BookingDate", booking.PreferredDate);
                bookingParams.Add("@ServiceType", booking.ServiceType);
                bookingParams.Add("@PickDrop", booking.PickupDrop);
                bookingParams.Add("@Status", "Pending"); // default status
                bookingParams.Add("@Remarks", booking.Notes);
                bookingParams.Add("@CreatedAt", DateTime.Now);

                await connection.ExecuteAsync(insertBookingQuery, bookingParams, transaction);

                transaction.Commit();
                return SuccessResponse("Vehicle and Booking added successfully.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ErrorResponse($"Operation failed: {ex.Message}");
            }
        }

        private IActionResult SuccessResponse(string message)
        {
            return new OkObjectResult(new { success = true, message });
        }

        private IActionResult ErrorResponse(string message)
        {
            return new BadRequestObjectResult(new { success = false, message });
        }
    }
}
