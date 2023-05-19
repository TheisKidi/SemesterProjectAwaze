using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;

namespace SemesterProjectAwaze.Pages.Login
{
    public class LogoutModel : PageModel
    {
        #region instance field
        private ILoginService _loginService;
        #endregion

        #region constructor
        public LogoutModel()
        {
        }
        #endregion

        #region method
        /// <summary>
        /// OnGet henter en profil og logger den herefter ud, sætter profilen til default.
        /// </summary>
        /// <returns>
        /// Returnerer index siden.
        /// </returns>
        public IActionResult OnGet()
        {
            _loginService = SessionHelper.GetProfile(HttpContext);
            _loginService.ProfileLoggedOut();
            SessionHelper.SetUser(_loginService, HttpContext);

            return RedirectToPage("../Index");
        }
        #endregion
    }
}
