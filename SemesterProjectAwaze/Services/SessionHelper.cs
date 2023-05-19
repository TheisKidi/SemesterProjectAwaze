using System.Text.Json;

namespace SemesterProjectAwaze.Services
{
    public class SessionHelper
    {
        #region instance field
        private const string KEY = "User";
        #endregion

        #region methods
        /// <summary>
        /// I metodens første linje bliver LoginService-objektet service 
        /// serialiseret til en JSON-streng ved hjælp af JsonSerializer-klassen. 
        /// I næste linje bliver JSON-strengen gemt som en session 
        /// i HttpContext-objektet.
        /// </summary>
        /// <param name="service">
        /// Tager et ILoginService-objekt ind
        /// </param>
        /// <param name="context">
        /// Tager et HttpContext-objekt som parametre
        /// </param>
        public static void SetUser(ILoginService service, HttpContext context)
        {
            string json = JsonSerializer.Serialize(service);
            context.Session.SetString(KEY, json);
        }

        /// <summary>
        /// Først bliver værdien af sessionvariablen med nøglen KEY hentet fra 
        /// HttpContext-objektets session. Herfter går den igennem en if sætning, som
        /// tjekker på om json ikke er null. Hvis JSON-strengen ikke er null, betyder 
        /// det, at sessionvariablen blev fundet og indeholder en JSON-repræsentation 
        /// af et LoginService-objekt. Hvis den er null oprettes der et nyt 
        /// LoginService-objekt ved hjælp af dens default konstruktør.
        /// </summary>
        /// <param name="context">
        /// Tager et HttpContext-objekt som parameter
        /// </param>
        /// <returns>
        /// returnerer et loginService objekt.
        /// </returns>
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
        #endregion
    }
}
