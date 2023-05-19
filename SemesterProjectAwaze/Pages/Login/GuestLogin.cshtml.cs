using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;

namespace SemesterProjectAwaze.Pages.Login
{
    public class GuestLoginModel : PageModel
    {
        #region instance field
        private ILoginService _loginService;
        private IGenericRepositoryService<Guest> _guestService;
        #endregion

        #region constructor
        public GuestLoginModel(IGenericRepositoryService<Guest> guestRepo)
        {
            _guestService = guestRepo;

        }
        #endregion

        #region properties
        [Required, BindProperty]
        public string Email { get; set; }
        [Required(ErrorMessage = "Adgangskode mangler"), BindProperty]
        public string Password { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// OnGet finder en profil via loginService og SessionHelper
        /// </summary>
        public void OnGet()
        {
            _loginService = SessionHelper.GetProfile(HttpContext);
        }

        /// <summary>
        /// F�est finder metoden en profil via SessionHelper. Herefter validerer den alle input.
        /// Metoden l�ber herefter igennem en foreach l�kke, som tager alle g�ster ind.
        /// Tjekker p� om emailen passer p� en email i listen. Herfeter verificerer den password,
        /// ved hj�lp af Crypto., som eren indbygget metode der tjekker passwordet mod det i databasen.
        /// Herfter try'er den SetProfileLoggedIn() metoden, hvis det ikke virker smider den en exception.
        /// Hvis den kommer ud af try, catch s�tter den profilen til at v�re logged in og omdiregerer g�sten
        /// til deres profil side. 
        /// </summary>
        /// <returns>
        /// Returnerer en side.
        /// </returns>
        public IActionResult OnPost()
        {
            _loginService = SessionHelper.GetProfile(HttpContext);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            foreach (Guest guest in _guestService.GetAll())
            {
                if (guest.Email == Email)
                {
                    if (Crypto.VerifyHashedPassword(guest.Password, Password))
                    {
                        try
                        {
                            _loginService.SetProfileLoggedIn(_guestService.GetByEmail(Email).FirstName, _guestService.GetByEmail(Email).MyBookingId, false);
                        }

                        catch (KeyNotFoundException ex)
                        {
                            return Page();

                        }
                        SessionHelper.SetUser(_loginService, HttpContext);
                        return RedirectToPage("GuestProfile");
                    }
                    ModelState.AddModelError("Password", "Adgangskode eller email er forkert");
                }
            }
            return Page();
        }
        #endregion
    }
}
