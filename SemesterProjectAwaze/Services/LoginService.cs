using AwazeLib.model;

namespace SemesterProjectAwaze.Services
{
    public class LoginService : ILoginService
    {
        private IGenericRepositoryService<Guest> _guestService;
        private IGenericRepositoryService<HouseOwner> _houseOwnerService;

        public LoginService(IGenericRepositoryService<Guest> guestService, IGenericRepositoryService<HouseOwner> houseOwnerService)
        {
            _guestService = guestService;
            _houseOwnerService = houseOwnerService;
        }
        public async Task<string> LoginAsync(string email, string password)
        {
            // chech HouseOwner
            var houseOwner = await _houseOwnerService.GetAll().FirstOrDefaultAsync()
        }
    }
}
