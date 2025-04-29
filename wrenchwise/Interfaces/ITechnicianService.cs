using wrenchwise.Models;

namespace wrenchwise.Interfaces
{
    public interface ITechnicianService
    {
        Task<IEnumerable<AdminTech>> GetAllRegisteredTechniciansAsync();
        Task<TechnicianResponse> AddTechnicianAsync(TechnicianRequest request);
    }
}
