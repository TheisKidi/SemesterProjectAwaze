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

        public Guest (string FirstName, string lastname, string email, string phone, bool isOwner, string password, string myBookingId)
            : base(FirstName, lastname, email, phone, isOwner, password)
        {
            MyBookingId = myBookingId;
        }

        public Guest() : this ("Default FirstName", "Default lastname", "Default email", "Default phone", false,
                               "Default Password", "DefaultBookingId" )
        {
        }

        public override string ToString()
        {
            return $"FirstName: {FirstName}, LastName: {LastName}, Email: {Email}, Phone: {Phone}, IsOwner: {IsOwner}, " +
                   $"Password: {Password}, MyBookingId: {MyBookingId}";
        }


    }
}
