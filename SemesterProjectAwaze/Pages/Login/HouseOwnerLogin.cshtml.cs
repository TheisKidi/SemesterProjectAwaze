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

        private ILoginService _loginService;
        private IGenericRepositoryService<HouseOwner> _repo;
        public HouseOwnerLoginModel(IGenericRepositoryService<HouseOwner> repo)
        {
            _repo = repo;
        }

        [Required, BindProperty]
        public string Email { get; set; }
        [Required, BindProperty]
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

            foreach(HouseOwner owner in _repo.GetAll())
            {
                if(owner.Email == Email)
                {
                    if (Crypto.VerifyHashedPassword(owner.Password, Password))
                    {
                        try
                        {
                            _loginService.SetProfileLoggedIn(_repo.GetByEmail(Email).FirstName, _repo.GetByEmail(Email).OwnerId, true);
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
    }
}
