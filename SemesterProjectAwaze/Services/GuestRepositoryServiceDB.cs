using AwazeLib.model;

namespace SemesterProjectAwaze.Services
{
    public class GuestRepositoryServiceDB : IGenericRepositoryService<Guest>
    {
        private GuestDBContext _db = new GuestDBContext();

       


        public Guest Create(Guest guest)
        {
            _db.Guest.Add(guest);
            _db.SaveChanges();
            return guest;
        }

        public Guest Delete(string id)
        {
            Guest? guest = GetById(id);
            _db.Guest.Remove(guest);
            _db.SaveChanges();
            return guest;
        }

        public List<Guest> GetAll()
        {
            return new List<Guest>(_db.Guest);
        }

        public Guest GetByEmail(string email)
        {
            Guest? guest = GetAll().FirstOrDefault(x => x.Email == email);
            if (guest == null)
            {
                throw new KeyNotFoundException(email);
            }
            return guest;
        }

        public Guest GetById(string id)
        {
            Guest? guest = GetAll().FirstOrDefault(x => x.MyBookingId == id);
            if (guest == null)
            {
                throw new KeyNotFoundException(id);
            }
            return guest;
        }

        public Guest Update(string id, Guest guest)
        {
            Guest? updateGuest = GetById(id);
            _db.Guest.Update(updateGuest);
            _db.SaveChanges();
            return guest;
        }


    }
}
