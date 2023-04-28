using AwazeLib.model;

namespace SemesterProjectAwaze.Services
{
    public interface IPropertyRepositoryService
    {
        public List<Property> GetAll();
        public Property GetById(int id);
        public Property Create(Property property);
        public Property Delete(string id);
        public Property Update(string id, Property property);
    }
}
