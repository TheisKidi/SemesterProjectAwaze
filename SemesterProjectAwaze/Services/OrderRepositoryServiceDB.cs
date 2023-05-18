using AwazeLib.model;
using Microsoft.EntityFrameworkCore;

namespace SemesterProjectAwaze.Services
{
    public class OrderRepositoryServiceDB
    {
        private OrderDBContext _db = new OrderDBContext();

        public Order Create(Order order)
        {
            _db.Order.Add(order);
            _db.SaveChanges();
            return order;
        }

        public Order Delete(int id)
        {
            Order? order = GetById(id);
            _db.Order.Remove(order);
            _db.SaveChanges();
            return order;
        }

        public Order GetById(int id)
        {
            Order? order = GetAll().FirstOrDefault(x => x.OrderId == id);
            if (order == null)
            {
                throw new KeyNotFoundException();
            }
            return order;
        }

        public List<Order> GetAll()
        {
            if (_db.Order == null)
            {
                throw new KeyNotFoundException();
            }
            return new List<Order>(_db.Order);
        }

        public List<Order> GetOrderByUserEmail(string email)
        {
            if (_db.Order == null)
            {
                throw new KeyNotFoundException();
            }
            return _db.Order.Include(f => f.Property).Where(f => f.Guest.Email == email).ToList();
        }
    }
}
