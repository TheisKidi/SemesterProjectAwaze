using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AwazeLib.model
{
    public class Favorite
    {
        #region properties
        public virtual Guest User { get; set; }
        public virtual Property Property { get; set; }
        public int Id { get; set; }
        [ForeignKey("Guest")]
        [MaxLength(6)]
        public string GuestId { get; set; }
        [ForeignKey("Property")]
        [MaxLength(6)]
        public string PropertyId { get; set; }
        #endregion

        #region constructors
        public Favorite(int id, string guestId, string propertyId)
        {
            Id = id;
            GuestId = guestId;
            PropertyId = propertyId;
        }

        public Favorite() : this(-1, "", "")
        {
        }
        #endregion
    }
}
