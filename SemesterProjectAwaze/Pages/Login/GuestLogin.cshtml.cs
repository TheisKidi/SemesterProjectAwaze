using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SemesterProjectAwaze.Services;
using System.ComponentModel.DataAnnotations;

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
        [Required, BindProperty]
        public string Password { get; set; }
        [Required, BindProperty]
        public string ConfirmPassword { get; set; }


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

            if (Password != ConfirmPassword)
            {
                return Page();
            }

            if (_guestRepo.GetByEmail(Email).IsOwner)
            {
                _loginService.SetProfileLoggedIn(Email, true);
            }
            else
            {
                _loginService.SetProfileLoggedIn(Email, false);
            }

            SessionHelper.SetUser(_loginService, HttpContext);
            return RedirectToPage("../Index");

        }


    }
}
