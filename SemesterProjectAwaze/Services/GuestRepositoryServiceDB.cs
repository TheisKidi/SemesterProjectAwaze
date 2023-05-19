using AwazeLib.model;

namespace SemesterProjectAwaze.Services
{
    public class GuestRepositoryServiceDB : IGenericRepositoryService<Guest>
    {
        #region instance field
        private GuestDBContext _db = new GuestDBContext();
        #endregion

        #region methods
        // forklares i FavoriteRepositoryService
        public Guest Create(Guest guest)
        {
            _db.Guest.Add(guest);
            _db.SaveChanges();
            return guest;
        }

        // forklares i FavoriteRepositoryService
        public Guest Delete(string id)
        {
            Guest? guest = GetById(id);
            _db.Guest.Remove(guest);
            _db.SaveChanges();
            return guest;
        }

        // forklares i FavoriteRepositoryService
        public List<Guest> GetAll()
        {
            return new List<Guest>(_db.Guest);
        }

        /// <summary>
        /// Bruges til at finde et objekt via en email.
        /// </summary>
        /// <param name="email">
        /// Tager en string ind som svarer til en gæste email
        /// </param>
        /// <returns>
        /// Returnerer en gæst
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// Smider en KeyNotFoundException() med email som parameter, hvis profilen ikke findes
        /// </exception>
        public Guest GetByEmail(string email)
        {
            Guest? guest = GetAll().FirstOrDefault(x => x.Email == email);
            if (guest == null)
            {
                throw new KeyNotFoundException(email);
            }
            return guest;
        }

        /// <summary>
        /// Bruges til at finde et objekt via et ID.
        /// </summary>
        /// <param name="id">
        /// Tager en string ind som svarer til et gæste ID
        /// </param>
        /// <returns>
        /// Returnerer en gæst
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// Smider en KeyNotFoundException() med id som parameter, hvis profilen ikke findes
        /// </exception>
        public Guest GetById(string id)
        {
            Guest? guest = GetAll().FirstOrDefault(x => x.MyBookingId == id);
            if (guest == null)
            {
                throw new KeyNotFoundException(id);
            }
            return guest;
        }

        /// <summary>
        /// Bruges til at opdatere et objekt.
        /// </summary>
        /// <param name="id">
        /// Tager et id ind på den gamle gæst
        /// </param>
        /// <param name="guest">
        /// Tager et nyt gæste objekt ind
        /// </param>
        /// <returns>
        /// Returnerer den opdaaterede gæst
        /// </returns>
        public Guest Update(string id, Guest guest)
        {
            Guest? updateGuest = GetById(id);
            _db.Guest.Update(updateGuest);
            _db.SaveChanges();
            return guest;
        }
        #endregion
    }
}
