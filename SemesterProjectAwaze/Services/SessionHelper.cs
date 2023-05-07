using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace SemesterProjectAwaze.Services
{
    public class SessionHelper
    {
        private const string KEY = "User";
        public static void SetUser(ILoginService service, HttpContext context)
        {
            string json = JsonSerializer.Serialize(service);
            context.Session.SetString(KEY, json);

        }

        public static ILoginService GetProfile(HttpContext context)
        {

            string? json = context.Session.GetString(KEY);

            if (json is not null)
            {

                return JsonSerializer.Deserialize<LoginService>(json);
            }
            else
            {
            LoginService us = new LoginService();
            us.ProfileLoggedOut();
            return us;
            }
        }
    }
}
