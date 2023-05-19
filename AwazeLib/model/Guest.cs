using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AwazeLib.model
{
    [Table("Guest")]
    public class Guest : Profile
    {
        #region properties
        [Key]
        [MaxLength(6)]
        public string MyBookingId { get; set; }
        #endregion

        #region constructors
        public Guest (string firstName, string lastname, string email, string phone, bool isOwner, string password, string myBookingId)
            : base(firstName, lastname, email, phone, isOwner, password)
        {
            MyBookingId = myBookingId;
        }

        public Guest() : this ("Default firstName", "Default lastname", "Default email", "Default phone", false,
                               "Default Password", "DefaultBookingId")
        {
        }
        #endregion

        #region toString
        public override string ToString()
        {
            return $"FirstName: {FirstName}, LastName: {LastName}, Email: {Email}, Phone: {Phone}, IsOwner: {IsOwner}, " +
                   $"Password: {Password}, MyBookingId: {MyBookingId}";
        }
        #endregion
    }
}
