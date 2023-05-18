using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwazeLib.model
{
    public class Order
    {
        public int OrderId { get; set; }
        public string MyBookingId { get; set; }
        public string PropertyId { get; set; }
        public Guest Guest { get; set; }
        public Property Property { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public decimal Price { get; set; }
        public bool IsBooked { get; set; }

        public Order(int orderId, string myBookingId, string propertyId, DateTime arrivalDate, DateTime departureDate, decimal price)
        {
            OrderId = orderId;
            MyBookingId = myBookingId;
            PropertyId = propertyId;
            ArrivalDate = arrivalDate;
            DepartureDate = departureDate;
            Price = price;
        }

        public Order() : this(0, "", "", DateTime.Now, DateTime.Now, 9) { }

        public override string ToString()
        {
            return $"Order Id: {OrderId}, Guest: {MyBookingId}, Property: {PropertyId}, Arrival Date: {ArrivalDate}, Departure Date: {DepartureDate}, Price: {Price}";
        }

    }
}
