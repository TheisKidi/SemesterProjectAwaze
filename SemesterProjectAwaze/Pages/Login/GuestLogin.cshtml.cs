using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SemesterProjectAwaze.Services;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;

namespace SemesterProjectAwaze.Pages.Login
{
    public class GuestLoginModel : PageModel
    {
        
        private ILoginService _loginService;
        private IGenericRepositoryService<Guest> _guestRepo;
        public GuestLoginModel(IGenericRepositoryService<Guest> guestRepo)
        {
            _guestRepo = guestRepo;

        }

        [Required, BindProperty]
        public string Email { get; set; }
        [Required(ErrorMessage = "Adgangskode mangler"), BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
            _loginService = SessionHelper.GetProfile(HttpContext);
        }

        public IActionResult OnPost()
        {
            _loginService = SessionHelper.GetProfile(HttpContext);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            foreach (Guest guest in _guestRepo.GetAll())
            {
                if (guest.Email == Email)
                {
                    if (Crypto.VerifyHashedPassword(guest.Password, Password))
                    {
                        try
                        {
                            _loginService.SetProfileLoggedIn(_guestRepo.GetByEmail(Email).FirstName, _guestRepo.GetByEmail(Email).MyBookingId, false);
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
    }
}
