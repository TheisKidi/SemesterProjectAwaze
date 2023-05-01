using AwazeLib.model;
using System.Data.SqlClient;

namespace SemesterProjectAwaze.Services
{
    public interface IPropertyRepositoryService
    {
        public List<Property> GetAll();
        public Property GetById(string id);
        public Property Create(Property property);
        public Property Delete(string id);
        public Property Update(string id, Property property);
    }
}
