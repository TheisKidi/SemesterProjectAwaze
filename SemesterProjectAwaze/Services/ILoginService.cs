using AwazeLib.model;

namespace SemesterProjectAwaze.Services
{
    public interface ILoginService
    {
        public bool Login(string email, string password);
        public Guest Guests { get; set; }
        public HouseOwner HouseOwners { get; set; }
    }
}
