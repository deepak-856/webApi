using wrenchwise.Models;
namespace wrenchwise.Interfaces
{
    public interface IRegisterService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    }
}
