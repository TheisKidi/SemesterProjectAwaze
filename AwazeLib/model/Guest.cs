using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AwazeLib.model
{
    [Table("Guest")]
    public class Guest : Profile
    {
        [Key]
        [MaxLength(6)]
        public string MyBookingId { get; set; }

        public Guest (string firstName, string lastname, string email, string phone, bool isOwner, string password, string myBookingId)
            : base(firstName, lastname, email, phone, isOwner, password)
        {
            MyBookingId = myBookingId;
        }

        public Guest() : this ("Default firstName", "Default lastname", "Default email", "Default phone", false,
                               "Default Password", "DefaultBookingId")
        {
        }

        public override string ToString()
        {
            return $"FirstName: {FirstName}, LastName: {LastName}, Email: {Email}, Phone: {Phone}, IsOwner: {IsOwner}, " +
                   $"Password: {Password}, MyBookingId: {MyBookingId}";
        }


    }
}
