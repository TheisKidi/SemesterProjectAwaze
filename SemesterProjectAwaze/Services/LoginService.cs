using AwazeLib.model;
using Microsoft.EntityFrameworkCore;

namespace SemesterProjectAwaze.Services
{
    public class LoginService : ILoginService
    {
        private HouseOwnerRepositoryServiceDB _houseOwnerService;

        public LoginService(HouseOwnerRepositoryServiceDB houseOwnerService)
        {
            _houseOwnerService = houseOwnerService;
        }
        public Task<string> LoginAsync(string email, string password)
        {
            // chech HouseOwner
            var houseOwner = _houseOwnerService.GetByEmail(email);
            return null;
        }
    }
}
