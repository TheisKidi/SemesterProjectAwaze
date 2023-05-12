using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwazeLib.model
{
    public class Favorite
    {
        public Guest User { get; set; }
        public Property Property { get; set; }
        public int Id { get; set; }

        public Favorite(int id, Guest guest, Property property)
        {
            Id = id;
            User = guest;
            Property = property;
        }
        public Favorite() : this(-1, new Guest(), new Property())
        { 
        }


    }
}
