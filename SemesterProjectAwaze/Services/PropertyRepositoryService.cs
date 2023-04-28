using AwazeLib.model;

namespace SemesterProjectAwaze.Services
{
    public class PropertyRepositoryService : IPropertyRepositoryService
    {
        private const string ConnectionString = "Data Source=mssql17.unoeuro.com;Initial Catalog=hasseh_com_db_awaze;User ID=hasseh_com;Password=********;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Property Create(Property property)
        {
            throw new NotImplementedException();
        }

        public Property Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Property> GetAll()
        {
            throw new NotImplementedException();
        }

        public Property GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Property Update(string id, Property property)
        {
            throw new NotImplementedException();
        }
    }
}
