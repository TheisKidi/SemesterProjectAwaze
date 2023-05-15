using AwazeLib.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace SemesterProjectAwaze.Services
{
    public class LoginService : ILoginService
    {
        public static bool iAmOwner = false;
        public static string MyName = "";
        public static string MyId = "";
        private LoggedInUser _user;

        public LoginService()
        {
            _user = new LoggedInUser();
        }

        public async void SetProfileLoggedIn(string name, string id, bool isOwner)
        {
            MyName = name;
            _user.Name = name;
            MyId = id;
            _user.Id = id;
            iAmOwner = isOwner;
            _user.IsOwner = isOwner;
        }

        public void ProfileLoggedOut()
        {
            MyName = "";
            _user.Name = "";
            MyId = "";
            _user.Id = "";
            _user.IsOwner = false;
            iAmOwner = false;
        }

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

    }
}
