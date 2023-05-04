using AwazeLib.model;

namespace SemesterProjectAwaze.Services
{
    public interface ILoginService
    {
        Task<string> LoginAsync(string email, string password);
    }
}
