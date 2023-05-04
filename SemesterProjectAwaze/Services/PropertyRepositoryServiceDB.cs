using AwazeLib.model;

namespace SemesterProjectAwaze.Services
{
    public class PropertyRepositoryServiceDB : IGenericRepositoryService<Property>
    {
        private PropertyDBContext _db = new PropertyDBContext();

        public Property Create(Property property)
        {
            _db.Property.Add(property);
            _db.SaveChanges();
            return property;
        }

        public Property Delete(string id)
        {
            Property? property = GetById(id);
            _db.Property.Remove(property);
            _db.SaveChanges();
            return property;
        }

        public List<Property> GetAll()
        {
            return new List<Property>(_db.Property);
        }

        public Property GetByEmail(string email)
        {
            return null;
        }

        public Property GetById(string id)
        {
            Property? property = GetAll().FirstOrDefault(x => x.Id == id);
            if (property == null)
            {
                throw new KeyNotFoundException(id);
            }
            return property;
        }

        public Property Update(string id, Property property)
        {
            Property? updateProperty = GetById(id);
            _db.Property.Update(updateProperty);
            _db.SaveChanges();
            return property;
        }
    }
}
