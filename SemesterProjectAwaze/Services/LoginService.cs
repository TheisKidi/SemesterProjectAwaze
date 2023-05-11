using AwazeLib.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace SemesterProjectAwaze.Services
{
    public class LoginService : ILoginService
    {
        public static bool iAmOwner = false;
        public static string MyName = "";
        public static string MyEmail = "";
        private LoggedInUser _user;

        public LoginService()
        {
            _user = new LoggedInUser();
        }

        public async void SetProfileLoggedIn(string name, string email, bool isOwner)
        {
            MyName = name;
            _user.Name = name;
            MyEmail = email;
            _user.Email = email;
            _user.IsOwner = isOwner;
            iAmOwner = isOwner;
        }

        public void ProfileLoggedOut()
        {
            MyName = "";
            _user.Name = "";
            MyEmail = "";
            _user.Email = "";
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

        public string Email
        {
            get { return _user.Email; }
            set { _user.Email = value; }
        }

    }
}
