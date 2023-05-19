using AwazeLib.model;
using Microsoft.EntityFrameworkCore;

namespace SemesterProjectAwaze.Services
{
    public class FavoriteRepositoryService
    {
        #region instance field
        private FavoriteDBContext _db = new FavoriteDBContext();
        #endregion

        #region methods
        /// <summary>
        /// Bruges til at tilføje objektet til databasen.
        /// </summary>
        /// <param name="favorite">
        /// Tager et fvorit objekt ind.
        /// </param>
        /// <returns>
        /// Returnerer et favorit objekt.
        /// </returns>
        public Favorite Create(Favorite favorite)
        {
            _db.Favorite.Add(favorite);
            _db.SaveChanges();
            return favorite;
        }

        /// <summary>
        /// Bruges til at fjerne et objekt fra databasen.
        /// </summary>
        /// <param name="id">
        /// Tager et id af objektet ind
        /// </param>
        /// <returns>
        /// Returnerer et objekt
        /// </returns>
        public Favorite Delete(int id)
        {
            Favorite? favorite = GetById(id);
            _db.Favorite.Remove(favorite);
            _db.SaveChanges();
            return favorite;
        }

        /// <summary>
        /// Finder et objekt via et ID
        /// </summary>
        /// <param name="id">
        /// Tager et ID ind
        /// </param>
        /// <returns>
        /// Returnerer et objekt
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// Smider en KeyNotFoundException() hvis den ikke kan finde noget.
        /// </exception>
        public Favorite GetById(int id)
        {
            Favorite? favorite = GetAll().FirstOrDefault(x => x.Id == id);
            if (favorite == null)
            {
                throw new KeyNotFoundException();
            }
            return favorite;
        }

        /// <summary>
        /// Finder ALLE objekter i listen
        /// </summary>
        /// <returns>
        /// Returnerer en liste
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// Smider en KeyNotFoundException() hvis den ikke kan finde noget.
        /// </exception>
        public List<Favorite> GetAll()
        {
            if (_db.Favorite == null)
            {
                throw new KeyNotFoundException();
            }
            return new List<Favorite>(_db.Favorite);
        }

        /// <summary>
        /// Finder alle favoriter via en gæste email
        /// </summary>
        /// <param name="email">
        /// tager en string ind svarende til en gæste email
        /// </param>
        /// <returns>
        /// Returnerer allle properties hvor gæsten har føjet dem til favorit
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// Smider en KeyNotFoundException() hvis den ikke kan finde noget.
        /// </exception>
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
        #endregion
    }
}
