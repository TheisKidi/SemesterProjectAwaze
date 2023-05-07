﻿using AwazeLib.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace SemesterProjectAwaze.Services
{
    public class LoginService : ILoginService
    {
        public static bool iAmOwner = false;
        private LoggedInUser _user;

        public LoginService()
        {
            _user = new LoggedInUser();
        }

        public async void SetProfileLoggedIn(string name, bool isOwner)
        {
            _user.Name = name;
            _user.IsOwner = isOwner;
            iAmOwner = isOwner;
        }

        public void ProfileLoggedOut()
        {
            _user.Name = "";
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
    }
}
