using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;

namespace SemesterProjectAwaze.Pages.Login
{
    public class HouseOwnerLoginModel : PageModel
    {
        #region instance field
        private ILoginService _loginService;
        private IGenericRepositoryService<HouseOwner> _houseOwnerService;
        #endregion

        #region constructor
        public HouseOwnerLoginModel(IGenericRepositoryService<HouseOwner> houseOwnerService)
        {
            _houseOwnerService = houseOwnerService;
        }
        #endregion

        #region properties
        [Required, BindProperty]
        public string Email { get; set; }
        [Required, BindProperty]
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
        /// Først finder metoden en profil via SessionHelper. Herefter validerer den alle input.
        /// Metoden løber herefter igennem en foreach løkke, som tager alle husejere ind.
        /// Tjekker på om emailen passer på en email i listen. Herfeter verificerer den password,
        /// ved hjælp af Crypto., som eren indbygget metode der tjekker passwordet mod det i databasen.
        /// Herfter try'er den SetProfileLoggedIn() metoden, hvis det ikke virker smider den en exception.
        /// Hvis den kommer ud af try, catch sætter den profilen til at være logged in og omdiregerer husejeren
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

            foreach(HouseOwner owner in _houseOwnerService.GetAll())
            {
                if(owner.Email == Email)
                {
                    if (Crypto.VerifyHashedPassword(owner.Password, Password))
                    {
                        try
                        {
                            _loginService.SetProfileLoggedIn(_houseOwnerService.GetByEmail(Email).FirstName, _houseOwnerService.GetByEmail(Email).OwnerId, true);
                        }
                        catch (KeyNotFoundException ex)
                        {
                            return Page();
                        }
                        SessionHelper.SetUser(_loginService, HttpContext);
                        return RedirectToPage("HouseOwnerProfile");
                    }
                    ModelState.AddModelError("Password", "Adgangskode eller email er forkert");
                }
            }
            return Page();
        }
        #endregion
    }
}
