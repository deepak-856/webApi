using Dapper;
using wrenchwise.Interfaces;
using wrenchwise.Models;
using Microsoft.AspNetCore.Identity.Data;
using Org.BouncyCastle.Asn1.Ocsp;
using wrenchwise.Utility;

namespace wrenchwise.Services
{

    public class RegisterService : IRegisterService
    {
        private readonly DBGateway _dbGateway;
        public RegisterService(DBGateway dbGateway)
        {
            _dbGateway = dbGateway;
        }
        public async Task<RegisterResponse> RegisterAsync(wrenchwise.Models.RegisterRequest request)
        {   
            var parameters = new DynamicParameters();

            parameters.Add("p_username", request.Username);
            parameters.Add("p_password", request.Password); // 🔐 hash this in production!
            parameters.Add("p_role_type", "User");
            parameters.Add("p_email", request.Email);
            parameters.Add("p_mobile", request.Mobile);
            parameters.Add("p_created_by", "User");


            try
            {
                var result = await _dbGateway.ExeSPScaler<int>("sp_register_user", parameters);

                return new RegisterResponse
                {
                    Success = true,
                    Message = "Registration successful"
                };
            }
            catch (Exception ex)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Message = $"Registration failed: {ex.Message}"
                };
            }
        }

        public async Task<IEnumerable<AdminUser>> GetAllRegisteredUsersAsync()
        {
            var result = await _dbGateway.QuerySPAsync<AdminUser>("sp_get_all_registered_users", null);
            return result;
        }





    }
}

