namespace AwazeLib.model
{
    public class LoggedInUser
    {
        #region properties
        public string Name { get; set; }
        public string Id { get; set; }
        public bool IsOwner { get; set; }
        #endregion

        #region constructors
        public LoggedInUser() : this("","", false)
        {
        }

        public LoggedInUser(string name, string id ,bool isAdmin)
        {
            Id = id;
            Name = name;
            IsOwner = isAdmin;
        }
        #endregion

        #region toString
        public override string ToString()
        {
            return $"Name: {Name}, Email: {Id}, IsAdmin: {IsOwner}";
        }
        #endregion
    }
}
