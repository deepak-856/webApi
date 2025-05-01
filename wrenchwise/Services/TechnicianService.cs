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
            parameters.Add("p_password", request.Password); // Consider hashing for production
            parameters.Add("p_role_type", "Technician");     // Hardcoded
            parameters.Add("p_email", request.Email);
            parameters.Add("p_mobile", request.Mobile);
            parameters.Add("p_specialization", request.Specialization);
            parameters.Add("p_address", request.Address);
            parameters.Add("p_experience_years", request.ExperienceYears);
            parameters.Add("p_created_by", "Technician");    // Hardcoded

            try
            {
                var result = await _dbGateway.ExeSPScaler<string>("sp_add_technician", parameters);

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

        public async Task<TechnicianResponse> UpdateTechnicianAsync(int loginId, TechnicianRequest request)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_login_id", loginId);
            parameters.Add("p_username", request.Name);
            parameters.Add("p_password", request.Password);
            parameters.Add("p_email", request.Email);
            parameters.Add("p_mobile", request.Mobile);
            parameters.Add("p_specialization", request.Specialization);
            parameters.Add("p_address", request.Address);
            parameters.Add("p_experience_years", request.ExperienceYears);

            try
            {
                var result = await _dbGateway.ExeSPScaler<string>("sp_update_technician", parameters);

                return new TechnicianResponse
                {
                    Success = true,
                    Message = result
                };
            }
            catch (Exception ex)
            {
                return new TechnicianResponse
                {
                    Success = false,
                    Message = $"Update failed: {ex.Message}"
                };
            }
        }


        public async Task<TechnicianResponse> DeleteTechnicianAsync(int loginId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_login_id", loginId);

            try
            {
                await _dbGateway.ExeSPScaler<string>("sp_delete_technician", parameters);
                return new TechnicianResponse { Success = true, Message = "Technician deleted successfully." };
            }
            catch (Exception ex)
            {
                return new TechnicianResponse { Success = false, Message = $"Delete failed: {ex.Message}" };
            }
        }


    }
}
