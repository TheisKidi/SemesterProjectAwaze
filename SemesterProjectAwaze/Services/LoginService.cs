using AwazeLib.model;

namespace SemesterProjectAwaze.Services
{
    public class LoginService : ILoginService
    {
        private IGenericRepositoryService<HouseOwner> _houseOwnerService;
        private IGenericRepositoryService<Guest> _guestService;

        public HouseOwner HouseOwners { get; set; }
        public Guest Guests { get; set; }

        public bool Login(string email, string password)
        {

            foreach (HouseOwner h in _houseOwnerService.GetAll())
            {
                if (h.Email == email && h.Password == password)
                {
                    HouseOwners = _houseOwnerService.GetByEmail(email);
                    break;
                }
                throw new ArgumentNullException();
            }




            //    foreach (HouseOwner ho in _houseOwnerService.GetAll())
            //    {
            //        if (ho.Email == email && ho.Password == password)
            //        {
            //            HouseOwners = _houseOwnerService.GetAll().Find(p => p.Email == email);
            //            return true;
            //        }
            //        else
            //        {
            //            foreach (Guest p in _guestService.GetAll())
            //            {
            //                if (p.Email == email && p.Password == password)
            //                {
            //                    Guests = _guestService.GetAll().Find(p => p.Email == email);
            //                    return true;
            //                }
            //            }

            //         return false;
            //        }
            //    }

            //    throw new KeyNotFoundException();
            //}
        }
    }
}
