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
        /// Føest finder metoden en profil via SessionHelper. Herefter validerer den alle input.
        /// Metoden løber herefter igennem en foreach løkke, som tager alle gæster ind.
        /// Tjekker på om emailen passer på en email i listen. Herfeter verificerer den password,
        /// ved hjælp af Crypto., som eren indbygget metode der tjekker passwordet mod det i databasen.
        /// Herfter try'er den SetProfileLoggedIn() metoden, hvis det ikke virker smider den en exception.
        /// Hvis den kommer ud af try, catch sætter den profilen til at være logged in og omdiregerer gæsten
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
