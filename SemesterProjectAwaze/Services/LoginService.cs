using AwazeLib.model;

namespace SemesterProjectAwaze.Services
{
    public class LoginService : ILoginService
    {
        #region instance field
        public static bool iAmOwner = false;
        public static string MyName = "";
        public static string MyId = "";
        private LoggedInUser _user;
        #endregion

        #region constructor
        public LoginService()
        {
            _user = new LoggedInUser();
        }
        #endregion

        #region properties
        public bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrEmpty(_user.Name);
            }
        }

        public bool IsProfileOwner
        {
            get { return _user.IsOwner; }
            set { _user.IsOwner = value; }
        }

        public string ProfileName
        {
            get { return _user.Name; }
            set { _user.Name = value; }
        }

        public string Id
        {
            get { return _user.Id; }
            set { _user.Id = value; }
        }
        #endregion

        #region methods
        /// <summary>
        /// Sætter en profil til at være logget ind.
        /// </summary>
        /// <param name="name">navn på profilen</param>
        /// <param name="id">id på profilen</param>
        /// <param name="isOwner">boolean på om profilen er ejer eller ej</param>
        public async void SetProfileLoggedIn(string name, string id, bool isOwner)
        {
            MyName = name;
            _user.Name = name;
            MyId = id;
            _user.Id = id;
            iAmOwner = isOwner;
            _user.IsOwner = isOwner;
        }

        /// <summary>
        /// Sætter en profil til at være logget ud ved at erstate alle properties
        /// med tomme værdier
        /// </summary>
        public void ProfileLoggedOut()
        {
            MyName = "";
            _user.Name = "";
            MyId = "";
            _user.Id = "";
            _user.IsOwner = false;
            iAmOwner = false;
        }
        #endregion
    }
}
