using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwazeLib.model
{
    public class Favorite
    {
        public virtual Guest User { get; set; }
        public virtual Property Property { get; set; }
        public int Id { get; set; }
        [ForeignKey("Guest")]
        [MaxLength(6)]
        public string GuestId { get; set; }
        [ForeignKey("Property")]
        [MaxLength(6)]
        public string PropertyId { get; set; }

        public Favorite(int id, string guestId, string propertyId)
        {
            Id = id;
            GuestId = guestId;
            PropertyId = propertyId;
        }

        public Favorite() : this(-1, "", "")
        {
        }


    }
}
