using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwazeLib.model
{
    public class Guest : Profile
    {
        public string MyBookingId { get; set; }

        public Guest (string surname, string lastname, string email, string phone, bool isOwner, string password, string myBookingId)
            : base(surname, lastname, email, phone, isOwner, password)
        {
            MyBookingId = myBookingId;
        }

        public Guest() : this ("Default surname", "Default lastname", "Default email", "Default phone", false, "Default Password", "DefaultBookingId" )
        {
        }

        public override string ToString()
        {
            return $"Surname: {SurName}, LastName: {LastName}, Email: {Email}, Phone: {Phone}, IsOwner: {IsOwner}, " +
                   $"Password: {Password}, MyBookingId: {MyBookingId}";
        }


    }
}
