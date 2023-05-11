using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwazeLib.model
{
    public class LoggedInUser
    {
        public string Name { get; set; }
        
        public string Email { get; set; }
        public bool IsOwner { get; set; }


        public LoggedInUser() : this("","", false)
        {
        }

        public LoggedInUser(string name, string email ,bool isAdmin)
        {
            Email = email;
            Name = name;
            IsOwner = isAdmin;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Email: {Email}, IsAdmin: {IsOwner}";
        }
    }
}
