using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;
using System.ComponentModel.DataAnnotations;

namespace SemesterProjectAwaze.Pages.Login
{
    public class GuestLoginModel : PageModel
    {
        private ILoginService _loginService;

        [BindProperty, Required(ErrorMessage = "Your password is requeried to log in")]
        public string Password { get; set; }
        [BindProperty, Required(ErrorMessage = "Your email is requeried to log in")]
        public string Email { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }

        public IActionResult OnPost()
        {
            var check = _loginService.Login(Email, Password);
            if (check && Password == ConfirmPassword)
            {
                return RedirectToPage("/Login/GuestSite");
            }

            return Page();
        }

        public GuestLoginModel(ILoginService loginService)
        {
            _loginService = loginService;
        }
    }
}
