using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AwazeLib.model
{
    public class HouseOwner : Profile
    {
        public string OwnerId { get; set; }
        public string Address { get; set; }

        public HouseOwner() : this("default surname", "default lastname", "default email", "default phone", 
                                   false, "default password", "default ownerId", "default address")
        {
        }

        public HouseOwner(string surname, string lastname, string email, 
               string phone, bool isOwner, string password, string ownerId, string address)
               : base(surname, lastname, email, phone, isOwner, password)
        {
            OwnerId = ownerId;
            Address = address;
        }

        public override string ToString()
        {
            return $"Surname: {SurName}, LastName: {LastName}, Email: {Email}, Phone: {Phone}, IsOwner: {IsOwner}, " +
                   $"Password: {Password}, OwnerId: {OwnerId}, Address: {Address}";
        }
    }
}
