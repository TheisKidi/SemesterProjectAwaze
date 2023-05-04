using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwazeLib.model
{
    public class Profile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsOwner { get; set; }
        public string Password { get; set; }

        public Profile() : this("default FirstName", "default lastname", "default email", "default phone", false, "default password")
        {
        }
        public Profile(string FirstName, string lastname, string email, string phone, bool isOwner, string password)
        {
            FirstName = FirstName;
            LastName = lastname;
            Email = email;
            Phone = phone;
            IsOwner = isOwner;
            Password = password;
        }

        public override string ToString()
        {
            return $"FirstName: {FirstName}, LastName: {LastName}, Email: {Email}, Phone: {Phone}, IsOwner: {IsOwner}, " +
                   $"Password: {Password}";
        }
    }
}
