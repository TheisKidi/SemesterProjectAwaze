using AwazeLib.model;
using Microsoft.EntityFrameworkCore;

namespace SemesterProjectAwaze.Services
{
    public class FavoriteRepositoryService
    {

        private FavoriteDBContext _db = new FavoriteDBContext();

        public Favorite Create(Favorite favorite)
        {
            _db.Favorite.Add(favorite);
            _db.SaveChanges();
            return favorite;
        }

        public Favorite Delete(int id)
        {
            Favorite? favorite = GetById(id);
            _db.Favorite.Remove(favorite);
            _db.SaveChanges();
            return favorite;
        }

        public Favorite GetById(int id)
        {
            Favorite? favorite = GetAll().FirstOrDefault(x => x.Id == id);
            if (favorite == null)
            {
                throw new KeyNotFoundException();
            }
            return favorite;
        }

        public List<Favorite> GetAll()
        {
            if (_db.Favorite == null)
            {
                throw new KeyNotFoundException();
            }
            return new List<Favorite>(_db.Favorite);
        }

        public List<Favorite> GetFavoritesByUserEmail(string email)
        {
            if (_db.Favorite == null)
            {
                throw new KeyNotFoundException();
            }

            return _db.Favorite.Include(f => f.Property).Where(f => f.User.Email == email).ToList();
        }

        public Favorite GetByEmail(string email)
        {
            return null;
        }

        public Favorite Update(string id, Favorite property)
        {
            return null;
        }

    }
}
