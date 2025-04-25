using wrenchwise.Models;
using System.Threading.Tasks;

namespace wrenchwise.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponse> Authenticate(LoginRequest request);
    }
}

