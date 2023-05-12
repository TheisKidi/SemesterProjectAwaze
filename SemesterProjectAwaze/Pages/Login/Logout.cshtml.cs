using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;

namespace SemesterProjectAwaze.Pages.Login
{
    public class LogoutModel : PageModel
    {
        private ILoginService _loginService;

        public LogoutModel()
        {

        }

        public IActionResult OnGet()
        {
            _loginService = SessionHelper.GetProfile(HttpContext);
            _loginService.ProfileLoggedOut();
            SessionHelper.SetUser(_loginService, HttpContext);

            return RedirectToPage("Index");
        }
    }
}
