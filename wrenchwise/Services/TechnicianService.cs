using Dapper;
using wrenchwise.Interfaces;
using wrenchwise.Models;
using wrenchwise.Utility;

namespace wrenchwise.Services
{
    public class TechnicianService : ITechnicianService
    {
        private readonly DBGateway _dbGateway;

        public TechnicianService(DBGateway dbGateway)
        {
            _dbGateway = dbGateway;
        }

        public async Task<TechnicianResponse> AddTechnicianAsync(TechnicianRequest request)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_name", request.Name);
            parameters.Add("p_password", request.Password); // Consider hashing for production!
            parameters.Add("p_role_type", "Technician");     // Hardcoded
            parameters.Add("p_email", request.Email);
            parameters.Add("p_mobile", request.Mobile);
            parameters.Add("p_created_by", "Technician");    // Hardcoded

            try
            {
                var result = await _dbGateway.ExeSPScaler<int>("sp_add_technician", parameters);

                return new TechnicianResponse
                {
                    Success = true,
                    Message = "Technician added successfully."
                };
            }
            catch (Exception ex)
            {
                return new TechnicianResponse
                {
                    Success = false,
                    Message = $"Failed to add technician: {ex.Message}"
                };
            }
        }
        public async Task<IEnumerable<AdminTech>> GetAllRegisteredTechniciansAsync()
        {
            var result = await _dbGateway.QuerySPAsync<AdminTech>("sp_get_all_technicians", null);
            return result;
        }
    }
}
