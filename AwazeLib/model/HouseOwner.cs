namespace AwazeLib.model
{
    public class HouseOwner : Profile
    {
        #region properties
        public string OwnerId { get; set; }
        public string Address { get; set; }
        #endregion

        #region constrctors
        public HouseOwner() : this("default FirstName", "default lastname", "default email", "default phone", 
                                   false, "default password", "default ownerId", "default address")
        {
        }

        public HouseOwner(string firstName, string lastname, string email, 
               string phone, bool isOwner, string password, string ownerId, string address)
               : base(firstName, lastname, email, phone, isOwner, password)
        {
            OwnerId = ownerId;
            Address = address;
        }
        #endregion

        #region toString
        public override string ToString()
        {
            return $"FirstName: {FirstName}, LastName: {LastName}, Email: {Email}, Phone: {Phone}, IsOwner: {IsOwner}, " +
                   $"Password: {Password}, OwnerId: {OwnerId}, Address: {Address}";
        }
        #endregion
    }
}
