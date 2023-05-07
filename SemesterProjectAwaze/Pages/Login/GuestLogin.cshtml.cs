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
        private IGenericRepositoryService<HouseOwner> _ownerRepo;
        public GuestLoginModel(IGenericRepositoryService<Guest> guestRepo, IGenericRepositoryService<HouseOwner> ownerRepo)
        {
            _guestRepo = guestRepo;
            _ownerRepo = ownerRepo;
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

            try
            {
                _guestRepo.GetByEmail(Email);
                _loginService.SetProfileLoggedIn(_guestRepo.GetByEmail(Email).FirstName, false);
            }
            catch (KeyNotFoundException ex)
            {
                return Page();

            }

            SessionHelper.SetUser(_loginService, HttpContext);
            return RedirectToPage("../Index");

        }


    }
}
