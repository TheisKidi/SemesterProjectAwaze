using AwazeLib.model;

namespace SemesterProjectAwaze.Services
{
    public class LoginService : ILoginService
    {
        private HouseOwnerRepositoryServiceDB _houseOwnerService;

        public LoginService(HouseOwnerRepositoryServiceDB houseOwnerService)
        {
            _houseOwnerService = houseOwnerService;
        }
        public async Task<string> LoginAsync(string email, string password)
        {
            // chech HouseOwner
            var houseOwner = await _houseOwnerService.GetAll().FirstOrDefaultAsync(ho => ho.Email == email);

        }
    }
}
