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
        public Guest Guest { get; set; }
        public Property Property { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public bool IsBooked { get; set; }

        public Order(int orderId, Guest guest, Property property, DateTime arrivalDate, DateTime departureDate, bool isBooked)
        {
            OrderId = orderId;
            Guest = guest;
            Property = property;
            ArrivalDate = arrivalDate;
            DepartureDate = departureDate;
            IsBooked = isBooked;
        }

        public Order() : this(0, new Guest(), new Property(), DateTime.Now, DateTime.Now, false) { }

        public override string ToString()
        {
            return $"Order Id: {OrderId}, Guest: {Guest}, Property: {Property}, Arrival Date: {ArrivalDate}, Departure Date: {DepartureDate}, Is Booked: {IsBooked}";
        }

    }
}
