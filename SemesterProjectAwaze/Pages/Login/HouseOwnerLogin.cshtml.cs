using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;
using System.ComponentModel.DataAnnotations;

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

            try
            {
                _repo.GetByEmail(Email);
                _loginService.SetProfileLoggedIn(_repo.GetByEmail(Email).FirstName, _repo.GetByEmail(Email).Email, true);
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
