using AwazeLib.model;
using System.Data.SqlClient;

namespace SemesterProjectAwaze.Services
{
    public interface IGenericRepositoryService<T>
    {
        public List<T> GetAll();
        public T GetById(string id);
        public T Create(T t);
        public T Delete(string id);
        public T Update(string id, T t);
        public T GetByEmail(string email);
    }
}