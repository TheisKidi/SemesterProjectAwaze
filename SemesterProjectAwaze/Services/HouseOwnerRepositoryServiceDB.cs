using AwazeLib.model;

namespace SemesterProjectAwaze.Services
{
    public class HouseOwnerRepositoryServiceDB : IGenericRepositoryService<HouseOwner>
    {
        #region instance field
        private HouseOwnerDBContext _db = new HouseOwnerDBContext();
        #endregion

        #region methods
        // alle metoder forklares i FavoriteRepositoryService samt GuestRepositoryService
        public HouseOwner Create(HouseOwner houseOwner)
        {
            _db.HouseOwner.Add(houseOwner);
            _db.SaveChanges();
            return houseOwner;
        }

        public HouseOwner Delete(string id)
        {
            HouseOwner? houseOwner = GetById(id);
            _db.HouseOwner.Remove(houseOwner);
            _db.SaveChanges();
            return houseOwner;
        }

        public List<HouseOwner> GetAll()
        {
            return new List<HouseOwner>(_db.HouseOwner);
        }

        public HouseOwner GetByEmail(string email)
        {
            HouseOwner? houseOwner = GetAll().FirstOrDefault(x => x.Email == email);
            if (houseOwner == null)
            {
                throw new KeyNotFoundException(email);
            }
            return houseOwner;
        }

        public HouseOwner GetById(string id)
        {
            HouseOwner? houseOwner = GetAll().FirstOrDefault(x => x.OwnerId == id);
            if(houseOwner == null) 
            { 
            throw new KeyNotFoundException(id);
            }
            return houseOwner;
        }

        public HouseOwner Update(string id, HouseOwner houseOwner)
        {
            HouseOwner? updateHouseOwner = GetById(id);
            _db.HouseOwner.Update(updateHouseOwner);
            _db.SaveChanges();
            return houseOwner;
        }
        #endregion
    }
}
